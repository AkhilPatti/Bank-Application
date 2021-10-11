using System;
using System.Collections.Generic;
namespace BankApp.Models
{
    public class Account
    {
        int accountId { get; set; }
        string accountHolderName { get; set; }
        public string pin { get; set; }
        string phoneNumber { get; set; }
        public int balance { get; set; }
        public List<Tuple<int, int, int>> tranctionHistory =new List<Tuple<int, int, int>>();
        public Account(int _accountId,string _accountHolderName ,string _pin,string _phoneNumber)
        {
            accountId = _accountId;
            accountHolderName = _accountHolderName;
            pin = _pin;
            phoneNumber = _phoneNumber;
            balance = 0;
        }

    }
}
