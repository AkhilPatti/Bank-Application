using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models.Exceptions;
using BankApp.Models;

namespace BankApp.Services
{
     public class BankService
       {
        //#pragma warning disable 0649
        private SqlHandler sqlHandler;
        BankDbContext db;
        public BankService() 
        {
            db = new BankDbContext();
            sqlHandler = new SqlHandler ();
            sqlHandler.StartConnection();
           
        }
        
        public string CreateBank(string name)
        {
            
            Bank bank = new Bank()
            {
                bankName = name,
                sImps = 0,
                sRtgs = 0,
                oImps = 0,
                
                bankId = GenerateRandomId(name),
                oRtgs = 0
            };
            db.Banks.Add(bank) ;
            
            return bank.bankId;
        }
    internal bool BankExists(string id)
        {
            try
            {
                var bank = db.Banks.Single(m => m.bankId == id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    public  string GenerateRandomId(string name)
    {
            name = name.ToUpper();
        string id = name.Substring(0,3);
            string date = DateTime.Now.ToString("ddMMyyyyHHmmss");
            id = id + date;
        return id;
    }

        public Bank FindBank(string bankId)
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

        public bool DeleteAccount(string accountId)
        {
            try
            {
                var account = db.Accounts.Single(m => m.accountId == accountId);
                db.Accounts.Remove(account);
                db.SaveChanges();
                return true;
            }
            catch
            {
                throw new AccountNotFound();
            }
        }

        public float AddCurrency(string currencyCode, string bankId)
        {

            try
            {
                var currency = db.Currencies.Single(m => m.currencyCode == currencyCode);
                float exchangeRate = (float)currency.exchangeRate;
                db.BankCurrencies.Add(new BankCurrencies
                    {
                    bankId = bankId,
                    currerncyCode = currencyCode
                });
                db.SaveChanges();
                return (float)exchangeRate;
            }
            catch
            {
                throw new InvalidCurrencyCode();
            }
        }

        public  void UpdateSameAccountCharges(float _impsCharge, float _rtgsCharge,string bankId)
        {
            Bank bank = FindBank(bankId);
            bank.sImps = _impsCharge;
            bank.sRtgs = _rtgsCharge;
            db.Banks.Update(bank);
            db.SaveChanges();
        }

        public void UpdateOtherAccountCharges(float _impsCharge, float _rtgsCharge, string bankId)
        {
            Bank bank = FindBank(bankId);
            bank.oImps = _impsCharge;
            bank.oRtgs = _rtgsCharge;
            db.Banks.Update(bank);
            db.SaveChanges();
        }

        public Account FindAccount(string _accountId)
        {
            try
            { Account account = db.Accounts.Single(m => m.accountId == _accountId);
                return account;
            }
            catch
            {
                throw new InvalidId();
            }
        }
        public string AddStaff(string bankId, string name, string password, Genders gender)
        {
            BankStaff staff = new BankStaff()
            {
                staffName = name,
                password = password,
                bankId = bankId,
                staffId = name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyyHHmmss")
        };
            db.Staff.Add(staff);
            return staff.staffId;
        }

        public bool AuthenticateBankStaff(string staffId, string password)
        {
            try
             {
                var staff = db.Staff.Single(s => s.staffId == staffId);
                if (String.Equals(staff.password, password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw new InvalidStaff();
            }   

        }

        public bool RevertTransaction(string transactionId)
        {

            Transaction transaction;
            try
            {
                transaction = db.Transactions.Single(m => m.tranactionId == transactionId);
            }
            catch
            {
                throw new InvalidTransactionId();
            }

            switch ((int)transaction.type)
            {
                case 0:
                        {
                            try
                            {
                                var account = FindAccount(transaction.sourceAccountId);
                                account.balance += transaction.amount;
                                db.Accounts.Update(account);
                                db.SaveChanges();
                                return true;
                            }
                            catch
                            {
                            throw new InvalidId();
                            }
                        
                    }
                case 2:
                    {
                        try
                        {
                            var account = FindAccount(transaction.receiveraccountId);

                            if (transaction.amount >= account.balance)
                            {
                                account.balance = transaction.amount;
                                db.Accounts.Update(account);
                                db.SaveChanges();
                                return true;
                            }
                            else { return false; }
                        }
                        catch
                        {
                            throw new InvalidReceiver();
                        }
                        
                    }
                case 3:
                    { Account saccount;
                        try
                        {
                            saccount = FindAccount(transaction.sourceAccountId);
                        }
                        catch
                        {
                            throw new InvalidId();
                        }
                        Account raccount;
                        try { raccount = FindAccount(transaction.receiveraccountId); }
                        catch { throw new InvalidReceiver(); };
                        if (raccount.balance < transaction.amount)
                        {
                            return false;
                        }
                        raccount.balance -= transaction.amount;
                        saccount.balance += transaction.amount;
                        db.Accounts.Update(raccount);
                        db.Accounts.Update(saccount);
                        break;
                    }
            }
            return true;
        }

       public List<(float amount, string sourceId, string recieverId,int type ,DateTime dateTime)> GetTransaction(string accountId)
        {
            List<(float, string, string, int, DateTime)> transactions = new List<(float, string, string, int, DateTime)>();
            var transaction = db.Transactions.Where(m => (m.receiveraccountId == accountId || m.sourceAccountId == accountId && m.type == TransactionType.Transfer))
                                .Select(m => new
                                {
                                    sourceAccountId = m.sourceAccountId ?? "",
                                    amount = m.amount,
                                    receiveraccountId = m.receiveraccountId ?? "",
                                    type = m.type==TransactionType.Deposit ? 0 : m.type==TransactionType.Transfer ? 1: 2,
                                    on = m.on
                                });
            foreach (var item in transaction)
            {
                transactions.Add((item.amount,item.sourceAccountId, item.receiveraccountId, (int)item.type, item.on));
            }
            return transactions;
            
        }
    }
}