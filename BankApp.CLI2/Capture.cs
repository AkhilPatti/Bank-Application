using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;
using BankApp.Models.Exceptions;
namespace BankApp.CLI2
{
    public static class Capture
    {
        public static  Genders CaptureGender()
        {
            Console.WriteLine("Enter your your Gender\n1. Male\n2.Female\n3.TransGender");
            Genders gender = (Genders)Enum.Parse(typeof(Genders), Console.ReadLine());
            return gender;
        }

        public static AccountType CaptureStatus()
        {
            Console.WriteLine("1.Saving \n2.Salary account\n3.FixedDeposit\n4.RecurringDeposit\n5.NRI");

            AccountType status = (AccountType)Enum.Parse(typeof(AccountType), Console.ReadLine());
            return status;
        }
        public static Tuple<string, string,string> CaptureDetails()
        {
            string id;
            string pin;
            Console.WriteLine("Enter a Valid BankID");
            string bankId = Console.ReadLine();
            Console.Write("Enter your AccountId : ");
            id = Console.ReadLine();
            Console.Write("Enter your Account Pin : ");
            pin = Console.ReadLine();
            return Tuple.Create(id, pin,bankId); ;
        }
        public static int CaptureAmount()
        {
            Console.WriteLine("Enter the Amount");
            int amount = Convert.ToInt32(Console.ReadLine());
            return amount;
        }
        public static string  CaptureCurrency(Bank bank)
        {
            Console.WriteLine("Choose code of the currencies Available");
            foreach(Currency currency in bank.currencies)
            {
                Console.WriteLine(currency.currencyCode);
            }
            string currencyCode=Console.ReadLine();
            try
            {
                Currency currency = bank.currencies.Single(m => m.currencyCode == currencyCode);
            }
            catch
            {
                throw new InvalidCurrencyCode();
            }
            return currencyCode;

        }
    }
}