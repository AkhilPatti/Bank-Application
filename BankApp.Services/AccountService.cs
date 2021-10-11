using System;
using System.Collections.Generic;

using BankApp.Models;
using BankApp.Models.Exceptions;
namespace BankApp.Services
{
    class AccountService
    {
        public List<Account> accounts= new List<Account>();
        public static int noOfAccounts=0;
        public void CreateAccount(string accountHolderName, string pin, string phoneNumber)
        {
            noOfAccounts ++;
            int accountId = noOfAccounts;
            Account  newAccount = new Account(accountId, accountHolderName, pin, phoneNumber);

            accounts.Add(newAccount);
        }
        public bool AccountValidator ( int id, string pin)
        {
            if(id>noOfAccounts)
            {
                throw new InvalidId();
            }
            else if(accounts[id].pin!=pin)
            {
                throw new InvalidPin();
            }
            else
            return true;
        }
        public void Deposit(int amount,int id,string pin)
        {
                if(AccountValidator(id,pin))
                {
                    accounts[id].balance += amount;
                    accounts[id].tranctionHistory.Add(new Tuple<int, int, int>(id, amount, accounts[id].balance));
            }

        }
        public void WithDraw (int amount,int id, string pin)
        {
            if (AccountValidator(id, pin))
            {
                if (accounts[id].balance < amount)
                {
                    throw new NotEnoughBalance();
                }
                else
                {
                    accounts[id].balance -= amount;
                    accounts[id].tranctionHistory.Add(new Tuple<int, int, int>(id, -amount, accounts[id].balance));
                }
            }
           
        }
        public void Transfer(int senderId, int receiverId,string senderPin,int amount)
        {
            if(AccountValidator(senderId,senderPin))
            {
                if(receiverId<noOfAccounts)
                {
                    accounts[senderId].balance -= amount;
                    //updating Sender's tranaction History
                    accounts[senderId].tranctionHistory.Add(new Tuple<int, int,int>(senderId, -amount, accounts[senderId].balance));
                    
                    accounts[receiverId].balance += amount;
                    //updating Receiver's tranaction History
                    accounts[receiverId].tranctionHistory.Add(new Tuple<int, int, int>(receiverId, amount, accounts[receiverId].balance));
                }
                else
                {
                    throw new InvalidReceiver();
                }
            }
        }

    }
}
