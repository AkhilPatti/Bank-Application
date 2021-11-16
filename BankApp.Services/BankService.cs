using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models.Exceptions;
using BankApp.Models;

namespace BankApp.Services
{
     public static class BankService
       { 
       internal static List<Bank> banks;
        
        public static string CreateBank(string name)
        {
            Bank bank = new Bank
            {
                id = GenerateRandomId(name),
                name = name
            };
            if (BankExists(bank.id))
            {
                
            }
            if (!BankExists(bank.id))
            {
                banks.Add(bank);
            }
            else
            {
                throw new BankAlreadyExists();
            }
            return bank.id;
        }
    internal static bool BankExists(string id)
        {
            try
            {
                Bank bank = banks.First(m => String.Equals(m.id, id));
                return true;
            }
            catch(InvalidOperationException)
            {
                return false;
            }
        }
    public static string GenerateRandomId(string name)
    {
        string id = name.Substring(0,3);
            string date = DateTime.Now.ToString("ddMMyyyy");
            id = id + date;
        return id;
    }

        public static Bank FindBank(string bankId)
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

        public static void DeleteAccount(string bankId,string accountId)
        {
            Bank bank = FindBank(bankId);
            Account account = FindAccount(bank, accountId);
            if (!bank.accounts.Remove(account))
            {
                throw new AccountNotFound();
            }
        }

        public static void AddCurrency(string _name,string _code, string bankId)
        {

            Currency currency = new Currency(_name, _code);

            Bank bank = FindBank(bankId);
            bank.currencies.Add(currency);

        }

        public static void UpdateSameAccountCharges(float _impsCharge, float _rtgsCharge,string bankId)
        {
            Bank bank = new Bank();
            bank= FindBank(bankId);
            bank.imps.sameAccountCharge = _impsCharge;
            bank.rtgs.sameAccountCharge = _rtgsCharge;
        }

        public static void UpdateOtherAccountCharges(float _impsCharge, float _rtgsCharge, string bankId)
        {
            Bank bank = new Bank();
            bank = FindBank(bankId);
            bank.imps.otherAccountCharge = _impsCharge;
            bank.rtgs.otherAccountcharge = _rtgsCharge;
        }

        public static Account FindAccount(Bank bank,string _accountId)
        {
            try
            { Account account = bank.accounts.Single(m => m.accountId == _accountId);
                return account;
            }
            catch
            {
                throw new InvalidId();
            }
        }
        public static Account getAccount(string _bankId,string _accountId)
        {
            Bank bank = new Bank();
            bank = FindBank(_bankId);
            Account account = new Account();
            account = FindAccount(bank, _accountId);
            return account;
        }

        public static bool AuthenticateBankStaff(string bankId, string staffId,string password)
        {
            Bank bank = FindBank(bankId);
            try
            {
                BankStaff staff = bank.staff.Single(m => m.staffId == staffId);
                if(String.Equals(staffId,staff.staffId))
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

        public static void RevertTransaction(string transactionId)
        {
            string bankId = transactionId.Substring(3, 14);
            string accountId = transactionId.Substring(15, 26);
            Bank bank = FindBank(bankId);
            Account account = FindAccount(bank,accountId);
            Transaction transaction = account.transactions.Single(m => m.tranactionId == transactionId);
            float amount = transaction.amount;
            TransactionType transactiontype = transaction.type;
            if(String.Equals(transaction.accountId,""))
            {
                if(transaction.type==TransactionType.Debit)
                account.balance += amount;
                else
                {
                    account.balance -= amount;
                }
            }
            else
            {
                string rBankId = transaction.recieverBankId;
                string rAccountId = transaction.accountId;
                Bank rBank = FindBank(rBankId);
                Account rAccount = FindAccount(rBank,rAccountId);
                rAccount.balance -= amount;
                account.balance += amount;

            }

        }
    }
}
