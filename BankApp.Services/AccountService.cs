using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;
using BankApp.Models.Exceptions;

namespace BankApp.Services
{
    public class AccountService
    {
        private SqlHandler sqlHandler;
        public void StartService()
        {
             sqlHandler = new SqlHandler();
            sqlHandler.StartConnection();
        }
       
        /*private static Bank FindBank(string bankId)
        {
            try
            {
                return BankService.banks.Single(m => m.id == bankId);
            }
            catch
            {
                throw new InvalidBankId();
            }
        }*/
        public string CreateAccount(string Name, string newPin,string phoneNo ,string bankId)
        {
            Console.WriteLine("Inside Accoun");
            string accountId = sqlHandler.AddUser(Name,phoneNo,newPin,bankId);
            Console.WriteLine("Fetched acccount Id {0}", accountId);
            if (String.Equals(accountId, "Error Occured"))
            {
                throw new InvalidBankId();
            }
            else
            {
                return accountId;
            }

        }
        public bool AccountValidator(string id, string pin)
        {
            
            //try
            //{
                
                return sqlHandler.AuthenticateUserExists(id, pin);
            /*}
            catch 
            {

                throw new InvalidId();
            }*/

        }

        private  bool AccountExists(string id)
        {

            // try
            //{
            Console.WriteLine(id);
                return sqlHandler.CheckUserExists(id);
            /*}
            catch
            {
                return false;
            }*/
        }

        /*public static Account FindAccount(string id, Bank bank)
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
        }*/
        public  float Deposit(float amount, string accountId, string pin,string currencyCode)
        {

            if (!AccountValidator(accountId, pin))
            {
                throw new InvalidId();
            }
            else
            {
                float balance = sqlHandler.DepositAmount(accountId, amount, currencyCode);

                return balance;
            }

        }
        public static float ConvertToRupees(string currencyCode, float amount,Bank bank)
        {
            Currency currency = bank.currencies.Single(m=>String.Equals(m.currencyCode,currencyCode));
            float newAmount = amount * currency.exchangeRate;
            Console.WriteLine(currency.exchangeRate);
            return newAmount;
        }
        public float WithDraw(float amount, string id, string pin)
        {
            if (AccountValidator(id, pin))
            {
                float balance = sqlHandler.GetBalance(id);
                if (balance < amount)
                {
                    throw new NotEnoughBalance();
                }
                else
                {
                    sqlHandler.WithdrawAmount(id, amount);
                }
            }
            else
            {
                throw new InvalidPin();
            }
            return sqlHandler.GetBalance(id);

        }

        public string Transfer(string senderId, string receiverId, string senderPin, float amount, string rbankId, string sbankId, TransactionService transactionService)
        {
            
            if (AccountValidator(senderId, senderPin))
            {
                if (AccountExists(receiverId))
                {

                    if ((int)transactionService == 1)
                    {
                        
                        return sqlHandler.TransferAmount(senderId, receiverId, amount, "IMPS");
                    }
                    else
                       return  sqlHandler.TransferAmount(senderId, receiverId, amount, "RTGS");
                }
                else
                {
                    throw new InvalidReceiver();
                }
            }
            throw new InvalidPin();

        }


        /*public static void UpdateTransaction(Account account, string sourceId, string recieverId, float _amount, TransactionType type, string sBankId,string rBankId)
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
        }*/
        public List<string> GetCurrencies (string bankId)
        {
            return sqlHandler.DisplayCurrencies(bankId);
        }
        public bool CheckCurrencyExist(string bankId, string currencyCode)
        {
            return sqlHandler.CheckAcceptedCurrency(bankId, currencyCode);
        }

        internal static string GenerateTransId(string date, string AccountId, string bankId)
        {
            return "TXN" + bankId + AccountId + date;
        }
        public List<(float , string , string , int, DateTime )> GetTransaction(string accountId)
        {
            return sqlHandler.GetTransaction(accountId);
        }

    }
    }

