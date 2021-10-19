using System;
using System.Collections.Generic;
namespace BankApp.Models
{
    public class Account
    {
        public  string accountId { get; set; }
        string accountHolderName { get; set; }
        public string pin { get; set; }
        public string phoneNumber { get; set; }
        public decimal balance { get; set; }
        public List<Transaction> Transactions { get; set; }
        

        public Genders Gender { get; set; }

        public AccountStatus status { get; set; }

        /*public Account(string _accountId, string _accountHolderName, string _pin, string _phoneNumber)
        {
            accountId = _accountId;
            accountHolderName = _accountHolderName;
            pin = _pin;
            phoneNumber = _phoneNumber;
            balance = 0;
        }*/
        
        
    }
    public enum Genders
    {
        Male = 1,
        Female,
        TransGendee,
    }
    public enum AccountStatus
    {
        Saving = 1,
        FixedDeposit,
        FIxedDeposit,
        RecurringDeposit,
        NRI,
    }
}
