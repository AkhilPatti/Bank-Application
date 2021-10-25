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
        
        /*public BankService()
        {
            this.banks = new List<Bank>();
        }*/
        public static string CreateBank(string name)
        {
            Bank bank = new Bank
            {
                Id = GenerateRandomId(name),
                Name = name
            };
            if (BankExists(bank.Id))
            {
                
            }
            if (!BankExists(bank.Id))
            {
                banks.Add(bank);
            }
            else
            {
                throw new BankAlreadyExists();
            }
            return bank.Id;
        }
    internal static bool BankExists(string id)
        {
            try
            {
                Bank bank = banks.First(m => String.Equals(m.Id, id));
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
    }
}
