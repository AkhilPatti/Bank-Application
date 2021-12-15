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
        public BankService() 
        { 
            sqlHandler = new SqlHandler ();
            sqlHandler.StartConnection();
           
        }
        
        public string CreateBank(string name)
        {
            string bankId = sqlHandler.AddBank(name);
            
            return bankId;
        }
    internal bool BankExists(string id)
        {
            try
            {
                if(sqlHandler.CheckBankExists(id))
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
                return false;
            }
        }
    public static string GenerateRandomId(string name)
    {
            name = name.ToUpper();
        string id = name.Substring(0,3);
            string date = DateTime.Now.ToString("ddMMyyyyHHmmss");
            id = id + date;
        return id;
    }

        /*public static Bank FindBank(string bankId)
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

        public bool DeleteAccount(string accountId)
        {
            if (sqlHandler.CheckUserExists(accountId))
            {
                throw new AccountNotFound();
            }
            return sqlHandler.DeleteUser(accountId);
        }

        public float AddCurrency(string _name,string _code, string bankId)
        {


            float ExchangeRate= sqlHandler.AddAcceptedCurrency(_code, bankId);

            return ExchangeRate;
        }

        public  void UpdateSameAccountCharges(float _impsCharge, float _rtgsCharge,string bankId)
        {

            sqlHandler.UpdatesIMPS(_impsCharge, bankId);
            
            sqlHandler.UpdatesRTGS(_rtgsCharge, bankId);
            
        }

        public void UpdateOtherAccountCharges(float _impsCharge, float _rtgsCharge, string bankId)
        {
            sqlHandler.UpdateoIMPS(_impsCharge, bankId);
            sqlHandler.UpdateoRTGS(_rtgsCharge, bankId);
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
        public string AddStaff (string _bankId,string _name,string _password,Genders _gender)
        {
            string staffId = sqlHandler.AddStaff(_name, _password, _bankId);
            return staffId;

        }

        public bool AuthenticateBankStaff(string staffId,string password)
        {
            
            return sqlHandler.AuthenticateStaff(staffId, password);

        }

        public bool RevertTransaction(string transactionId)
        {
            return sqlHandler.RevertTransaction(transactionId);

        }
       public List<(float amount, string sourceId, string recieverId,int type ,DateTime dateTime)> GetTransaction(string accountId)
        {
            return sqlHandler.GetTransaction(accountId);
        }
    }
}