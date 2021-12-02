using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;
using BankApp.Models.Exceptions;
namespace BankApp.Services
{
    public static class AccountService
    {
        public static string bankId;
       
        private static Bank FindBank(string bankId)
        {
            try
            {
                return BankService.banks.Single(m => m.id == bankId);
            }
            catch
            {
                throw new InvalidBankId();
            }
        }
        public static string CreateAccount(string Name, string newPin, string Number, string bankId)
        {
            Bank bank = FindBank(bankId);
            Account account = new Account
            { accountId = BankService.GenerateRandomId(Name),
                pin = newPin,
                phoneNumber = Number,
                transactions = new List<Transaction>()
            };
            if (!AccountExists(account.accountId, bank))
            {
                bank.accounts.Add(account);

                return account.accountId;
            }
            throw new InvalidId();


        }
        public static bool AccountValidator(string id, string pin, Account account)
        {
            
            try
            {
                //Account account = bank.accounts.First(m => String.Equals(m.accountId, id));
                if (account.pin != pin)
                {

                    throw new InvalidPin();
                }
                else
                {
                    return true;
                }
            }
            catch (InvalidOperationException)
            {

                throw new InvalidId();
            }

        }

        private static bool AccountExists(string id, Bank bank)
        {
            
            try
            {
                Account account = bank.accounts.First(m => String.Equals(m.accountId, id));
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        public static Account FindAccount(string id, Bank bank)
        {
                
                if (AccountExists(id, bank))
                {
                    Account account = bank.accounts.Single(m => String.Equals(m.accountId, id));
                    return account;
                }
            else
            {
                throw new InvalidId();
            }


        }
        public static float Deposit(float amount, string id, string pin, string bankId,string currencyCode)
        {

            Bank bank = FindBank(bankId);
            Account account = FindAccount(id, bank);
            amount = ConvertToRupees(currencyCode, amount, bank);
            if (AccountValidator(id, pin, account))
            {
                account.balance += amount;
                UpdateTransaction(account, "", id, amount, TransactionType.Debit, bankId,"");
                
            }
            else
            {
                throw new InvalidPin();
            }
            return account.balance;

        }
        public static float ConvertToRupees(string currencyCode, float amount,Bank bank)
        {
            Currency currency = bank.currencies.Single(m=>String.Equals(m.currencyCode,currencyCode));
            float newAmount = amount * currency.exchangeRate;
            Console.WriteLine(currency.exchangeRate);
            return newAmount;
        }
        public static  float WithDraw(float amount, string id, string pin, string bankId)
        {
            Bank bank = FindBank(bankId);
            Account account = FindAccount(id,bank);
            if (AccountValidator(id, pin, account))
            {
                if (account.balance < amount)
                {
                    throw new NotEnoughBalance();
                }
                else
                {
                    account.balance -= amount;
                    UpdateTransaction(account, "", id, amount, TransactionType.Credit, bankId,"");
                }
            }
            else
            {
                throw new InvalidPin();
            }
            return account.balance;

        }
        public static void Transfer(string senderId, string receiverId, string senderPin, float amount, string rbankId, string sbankId,TransactionService transactionService)
        {
            Bank rbank = FindBank(rbankId);
            Bank sbank = FindBank(sbankId);
            Account account = FindAccount(senderId,sbank);
            if (AccountValidator(senderId, senderPin, account))
            {
                if (AccountExists(receiverId, rbank))
                {
                    Account Raccount = rbank.accounts.First(m => String.Equals(m.accountId, receiverId));
                    
                    //updating Sender's tranaction History
                    UpdateTransaction(Raccount, senderId, receiverId, amount, TransactionType.Debit, sbankId,rbankId);

                    Account Saccount = sbank.accounts.First(m => String.Equals(m.accountId, senderId));
                    float charge = Calculatecharge(amount, sbank, sbankId, transactionService);
                    Raccount.balance += amount;
                    Saccount.balance -= (amount+charge);
                    
                    //updating Receiver's tranaction History
                    UpdateTransaction(Saccount, receiverId, senderId, amount, TransactionType.Credit, rbankId,sbankId);
                }
                else
                {
                    throw new InvalidReceiver();
                }
            }

        }


        public static void UpdateTransaction(Account account, string sourceId, string recieverId, float _amount, TransactionType type, string sBankId,string rBankId)
        {
            Transaction transaction = new Transaction
            {
                sourceAccountId = sourceId,
                accountId = recieverId,
                amount = _amount,
                type = type,
                on = DateTime.Now,
                sourceBankId = sBankId,
                recieverBankId =rBankId,
                tranactionId = GenerateTransId(DateTime.Now.ToString("ddMMyyyy"), sourceId, sBankId),

            };
            account.transactions.Add(transaction);

        }

        internal static float Calculatecharge(float amount, Bank bank, string sbankId, TransactionService transactionService)
        {
            string rbankId = bank.id;
            if (transactionService == TransactionService.IMPS)
            {
                if (String.Equals(rbankId, sbankId))
                {
                    amount=  amount * bank.imps.sameAccountCharge;
                }
                else
                {
                    amount= amount * bank.imps.sameAccountCharge;
                }
            }
            else if (transactionService == TransactionService.RTGS)
            {
                if (String.Equals(rbankId, sbankId))
                {
                   amount= amount * bank.rtgs.sameAccountCharge;
                }
                else
                {
                    amount=  amount * bank.rtgs.sameAccountCharge;
                }
            }
            return amount;
        }
        internal static string GenerateTransId(string date, string AccountId, string bankId)
        {
            return "TXN" + bankId + AccountId + date;
        }

       }
    }

