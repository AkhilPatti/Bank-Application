using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;
using BankApp.Models.Exceptions;
namespace BankApp.Services
{
    public static class AccountService
    {
        //private List<Account> accounts;
        public static string bankId;
        /*public static AccountService()
        {
            accounts = new List<Account>();
        }*/
        private static Bank FindBank(string bankId)
        {
            try
            {
                return BankService.banks.Single(m => m.Id == bankId);
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

            };
            if (!AccountExists(account.accountId, bankId))
            {
                bank.Accounts.Add(account);

                return account.accountId;
            }
            throw new InvalidId();


        }
        private static bool AccountValidator(string id, string pin, string bankId)
        {
            Bank bank = FindBank(bankId);
            try
            {
                Account account = bank.Accounts.First(m => String.Equals(m.accountId, id));
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

        private static bool AccountExists(string id, string bankId)
        {
            Bank bank = FindBank(bankId);
            try
            {
                Account account = bank.Accounts.First(m => String.Equals(m.accountId, id));
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public static void Deposit(int amount, string id, string pin, string bankId)
        {

            Bank bank = FindBank(bankId);
            if (AccountValidator(id, pin, bankId))
            {
                Account account = bank.Accounts.Single(m => String.Equals(m.accountId, id));
                account.balance += amount;
                UpdateTransaction(account, "", id, amount, TransactionType.Debit, bankId);
            }

        }
        public static void WithDraw(int amount, string id, string pin, string bankId)
        {
            Bank bank = FindBank(bankId);
            if (AccountValidator(id, pin, bankId))
            {
                Account account = bank.Accounts.First(m => String.Equals(m.accountId, id));
                if (account.balance < amount)
                {
                    throw new NotEnoughBalance();
                }
                else
                {
                    account.balance -= amount;
                    UpdateTransaction(account, "", id, amount, TransactionType.Credit, bankId);
                }
            }

        }
        public static void Transfer(string senderId, string receiverId, string senderPin, int amount, string bankId)
        {
            Bank bank = FindBank(bankId);
            if (AccountValidator(senderId, senderPin, bankId))
            {
                if (AccountExists(receiverId, bankId))
                {
                    Account Raccount = bank.Accounts.First(m => String.Equals(m.accountId, receiverId));
                    Raccount.balance += amount;
                    //updating Sender's tranaction History
                    UpdateTransaction(Raccount, senderId, receiverId, amount, TransactionType.Debit, bankId);

                    Account Saccount = bank.Accounts.First(m => String.Equals(m.accountId, senderId));
                    Saccount.balance += amount;
                    //updating Receiver's tranaction History
                    UpdateTransaction(Saccount, receiverId, senderId, amount, TransactionType.Credit, bankId);
                }
                else
                {
                    throw new InvalidReceiver();
                }
            }

        }

        public static void UpdateTransaction(Account account, string sourceId, string recieverId, int amount, TransactionType type, string bankId)
        {
            Transaction transaction = new Transaction
            {
                SourceAccountId = sourceId,
                AccountId = recieverId,
                Amount = amount,
                Type = type,
                On = DateTime.Now,
                TranactionId = GenerateTransId(DateTime.Now.ToString("ddMMyyyy"), recieverId, bankId),

            };
            account.transactions.Add(transaction);

        }

        internal static string GenerateTransId(string date, string AccountId, string bankId)
        {
            return "TXN" + bankId + AccountId + date;
        }
        public static List<Tuple<string, string, TransactionType, DateTime, int>> getTranactionDetails(string BankId, string AccountId, string pin)
        {

            var transList = new List<Tuple<string, string, TransactionType, DateTime, int>>();
            if (AccountValidator(AccountId, pin, bankId))
            {

                Bank bank = FindBank(BankId);
                Account account = bank.Accounts.Single(m => m.accountId == AccountId);
                foreach (Transaction trans in account.transactions)
                {
                    transList.Add(Tuple.Create(trans.SourceAccountId, trans.AccountId, trans.Type, trans.On, trans.Amount));
                }
            }
            return transList;
        }


       }
    }

