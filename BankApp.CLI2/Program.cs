using System;
using BankApp.Services;
using BankApp.Models.Exceptions;
using System.Collections.Generic;
using BankApp.Models;

namespace BankAppCLI2
{
    public enum UserType
    {
        AccountHolder=1,
        BankStaff,
        Exit
    };
    class Program
    {
        static void Main(string[] args)
        {


            bool TryAgain = true;
            while (TryAgain)
            {
                Choice.display();
                UserType userOption = (UserType)Enum.Parse(typeof(UserType), Console.ReadLine());
                switch (userOption)
                {
                    case UserType.AccountHolder:
                        {
                            AccountHolderPage accountHolderPage = new AccountHolderPage();
                            accountHolderPage.Display();
                            TryAgain = false;
                            break;
                        }
                    case UserType.BankStaff:
                        {
                            Console.WriteLine("Enter your Bank ID");
                            string bankId = Console.ReadLine();
                            Console.WriteLine("Enter your StaffId");
                            string staffId=Console.ReadLine();
                            Console.WriteLine("Enter the password");
                            string password = Console.ReadLine();
                            if(BankService.AuthenticateBankStaff(bankId, staffId, password))
                            {
                                string _bankId = bankId;
                                StaffPage staff = new StaffPage
                                {
                                    bankId = _bankId
                                };
                                staff.Display();
                            }
                            TryAgain = false;
                            break;

                        }
                    case UserType.Exit:
                        {
                            TryAgain = false;
                            Console.WriteLine("THank you");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Enter Valid Options");
                            break;
                        }

                }
            }
    }
}
    }