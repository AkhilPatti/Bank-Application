using System;
using BankApp.Services;
using System.Linq;
using BankApp.Models.Exceptions;
using System.Collections.Generic;
using BankApp.Models;

namespace BankApp.CLI2
{
    public enum UserType
    {
        AccountHolder=1,
        BankStaff,
        CreateBank,
        Exit
    };
    class Program
    {
        static void Main(string[] args)
        {
           
            BankService bankService = new BankService();
            
            bool TryAgain = true;
            while (TryAgain)
            {
                Choice.Display();
                UserType userOption = (UserType)Enum.Parse(typeof(UserType), Console.ReadLine());
                switch (userOption)
                {
                    case UserType.AccountHolder:
                        {
                            AccountHoldersPage accountHolderPage = new AccountHoldersPage();
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
                            try
                            {

                            if (bankService.AuthenticateBankStaff(staffId, password))
                            {
                                Console.WriteLine("Correct pass");
                                string _bankId = bankId;
                                    StaffPage staff = new StaffPage(bankService,_bankId);
                                {
                                        bankId = _bankId;
                                }
                                staff.Display();
                            }
                            else
                            {
                                Console.WriteLine("Invalid Details");
                            }
                            
                        }
                            catch(InvalidBankId)
                            {
                                Console.WriteLine("Please Enter Valid BankId");
                            }
                            catch(InvalidStaff)
                            {
                                Console.WriteLine("Please Enter Valid StaffId");
                            }
                            
                            break;

                        }
                    case UserType.CreateBank:
                        {
                            Console.WriteLine("Enter the name of the bank");
                            string bankName=Console.ReadLine();


                            try { 
                                string bankId = bankService.CreateBank(bankName);
                                Console.WriteLine("Your bank Id is {}", bankId);
                            }
                            catch (BankAlreadyExists)
                            {
                                Console.WriteLine("Bank with the above name name Akready exists");
                            }


                            
                            break;
                        }
                    case UserType.Exit:
                        {
                            TryAgain = false;
                            Console.WriteLine("Thank you");
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