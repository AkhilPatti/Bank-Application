using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;
using BankApp.Models.Exceptions;
namespace BankApp.Services
{
    public class AccountService
    {
        
        BankDbContext db;
        public void StartService()
        {
            db = new BankDbContext();
            
        }
       
        private  Bank FindBank(string bankId)
        {
            try
            {
                return db.Banks.Single(m => m.bankId == bankId);
            }
            catch
            {
                throw new InvalidBankId();
            }
        }
        public string CreateAccount(string name, string newPin,string phoneNo ,string bankId)
        {
            string accountId = name.Substring(0, 3) + bankId.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyyHHmmss");
            db.Add(new Account
            {
                accountHolderName = name,
                pin = newPin,
                phoneNumber = phoneNo,
                balance = 0,
                bankId = bankId,
                accountId = accountId,
        }) ;
            db.SaveChanges();
                return accountId;
            

        }
        public bool AccountValidator(string id, string pin)
        {
            
            try
            {
                var Account = db.Accounts.Single(m => m.accountId == id);
                if(String.Equals(Account.pin,pin))
                {
                    return true;
                }
                return false;
            }
            catch 
            {

                throw new InvalidId();
            }

        }

        private  bool AccountExists(string id)
        {

            try
            {
            
                var Account = db.Accounts.Single(m => m.accountId == id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Account FindAccount(string id)
        {
                
            if (AccountExists(id))
            {
                Account account = db.Accounts.Single(m => String.Equals(m.accountId, id));
                return account;
            }
            else
            {
                throw new InvalidId();
            }
        }
        public  float Deposit(float amount, string accountId, string pin,string currencyCode)
        {

            if (!AccountValidator(accountId, pin))
            {
                throw new InvalidId();
            }
            else
            {
                var account = FindAccount(accountId);
                account.balance += amount;
                db.Update(account);
                db.SaveChanges();
                return account.balance;
            }

        }
        public float ConvertToRupees(string currencyCode, float amount,Bank bank)
        {
            var currency = db.Currencies.Single(m=>String.Equals(m.currencyCode,currencyCode));
            float newAmount = amount * (float)currency.exchangeRate;
            Console.WriteLine(currency.exchangeRate);
            return newAmount;
        }
        public float WithDraw(float amount, string id, string pin)
        {
            if (AccountValidator(id, pin))
            {
                var account = FindAccount(id);
                float balance = account.balance;
                if (balance < amount)
                {
                    throw new NotEnoughBalance();
                }
                else
                {
                    account.balance -= amount;
                    db.Update(account);
                    db.SaveChanges();
                    return account.balance;
                }
            }
            else
            {
                throw new InvalidPin();
            }
            

        }

        public string Transfer(string senderId, string receiverId, string senderPin, float amount, string rbankId, string sbankId, TransactionService transactionService)
        {
            
            if (AccountValidator(senderId, senderPin))
            {
                if (AccountExists(receiverId))
                {

                    Account saccount = FindAccount(senderId);
                    Account raccount = FindAccount(receiverId);
                    Bank sbank = FindBank(saccount.bankId);
                    if (raccount.balance < amount)
                    { throw new NotEnoughBalance(); }
                    double charge;
                    if ((int)transactionService == 1)
                    {

                        if (string.Equals(raccount.bankId, saccount.bankId))
                        { charge = sbank.sImps; }
                        else
                        {
                            charge = sbank.oImps;
                        }
                    }

                    else
                    {

                        if (string.Equals(raccount.bankId, saccount.bankId)) { charge = sbank.sRtgs; }
                        else
                        {
                            charge = sbank.oRtgs;
                        }
                    }
                        raccount.balance += (float)(amount - (amount * charge));
                        saccount.balance -= (float)(amount - (amount * charge));
                    Transaction transaction = new Transaction
                    {
                        sourceAccountId = senderId,
                        receiveraccountId = receiverId,
                        amount = amount,
                        type = TransactionType.Transfer,
                        on = DateTime.Now,
                        tranactionId = GenerateTransId(DateTime.Now.ToString("ddMMyyyyHHmmss"), senderId, sbankId),

                    };
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    return transaction.tranactionId;
                    }
                else
                {
                    throw new InvalidReceiver();
                }

                }
                else
                {
                    throw new InvalidReceiver();
                }
            }
        
        public List<string> GetCurrencies (string bankId)
        {
            List <string> currencies = new List<string>();
            var bankcurrencies = db.BankCurrencies.Where(m => m.bankId==bankId).ToList();
            foreach(var item in bankcurrencies)
            {
                currencies.Add(item.currerncyCode);
            }
            return currencies;
        }
        public bool CheckCurrencyExist(string bankId, string currencyCode)
        {
            try
            {
                var currency = db.BankCurrencies.Single(m => (String.Equals(bankId, m.bankId) && (String.Equals(currencyCode, m.currerncyCode))));
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal string GenerateTransId(string date, string AccountId, string bankId)
        {
            return "TXN" + bankId + AccountId + date;
        }
        public List<(float , string , string , int, DateTime )> GetTransaction(string accountId)
        {
            List<(float, string, string, int, DateTime)> transactions = new List<(float, string, string, int, DateTime)>();
            var transaction = db.Transactions.Where(m => ((m.receiveraccountId == accountId) || (m.sourceAccountId == accountId))).ToList();
            foreach ( var item in transaction)
            {
                transactions.Add((item.amount, item.sourceAccountId, item.receiveraccountId, (int)item.type, item.on));
            }
            return transactions;
        }

    }
    }

