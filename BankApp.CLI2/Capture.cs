using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models;
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
    }
}