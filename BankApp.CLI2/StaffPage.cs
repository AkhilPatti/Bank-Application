using System;
using System.Collections.Generic;
using BankApp.Services;
using BankApp.Models;
using BankApp.Models.Exceptions;

namespace BankApp.CLI2
{
    public enum StaffOptions
    {
        CreateAccount = 1,
        DeleteAccount,
        AddCurrency,
        ChargeofSameAccount,
        ChargeofOtherAccont,
        ViewUserTransactionHistory,
        RevertTranaction,
        Exit,
    }
    public class StaffPage
    {
        public string bankId { get; set; }
        public BankService bankService;
        AccountService accountService;
        public StaffPage(BankService _bankService,string _bankId)
        {
            bankId = _bankId;
            bankService = _bankService;
            accountService = new AccountService();
            

        }


        public void Display()
        {
            
            
            bool tryAgain = true;
            while (tryAgain)
            {
                Console.WriteLine("Chosse from Below Options");
                Console.WriteLine("1.CreateAccount = 1\n2.DeleteAccount\n3.AddCurrency\n4.ChargeofSameAccount\n5.ChargeofOtherAccont\n6.ViewUserTransactionHistory\n7.RevertTranaction");
                StaffOptions choosen = (StaffOptions)Enum.Parse(typeof(StaffOptions), Console.ReadLine());
                switch (choosen)
                {
                    case StaffOptions.CreateAccount:
                        {
                            try
                            {
                                Console.WriteLine("Enter Your Name");
                                string name = Console.ReadLine();
                                Console.WriteLine("Enter the pin you want to set");
                                string pin = Console.ReadLine();
                                Console.WriteLine("Enter the Phone Number");
                                string phoneNo = Console.ReadLine();

                                //string id = accountService.CreateAccount(name, pin,phoneNo, bankId);
                                string id= "sdasda";
                                Console.WriteLine("ID is {0}",id);
                                Console.WriteLine("Your Account id is {0} with BankId as {1}", id, bankId);
                                
                            }

                            catch
                            {
                                Console.WriteLine("Enter Valid Details");
                            }
                            break;
                        }
                    case StaffOptions.DeleteAccount:
                        {
                            try
                            {
                                Console.WriteLine("Enter the Id of the Account you Want to Delete");
                                string accountId = Console.ReadLine();

                                bankService.DeleteAccount(accountId);

                                Console.WriteLine("Account Deleted Successfully");
                            }
                            catch(InvalidId)
                            {
                                Console.WriteLine("Please Enter a Valid AccountId");
                            }
                            break;
                        }
                    case StaffOptions.AddCurrency:
                        {
                            try
                            {
                                Console.WriteLine("Enter the Name of the New Currency");
                                string name = Console.ReadLine();
                                Console.WriteLine("Enter its currency code ");
                                string code = Console.ReadLine();
                                code = code.ToLower();
                                bankService.AddCurrency(code, bankId);
                            }
                            catch( InvalidCurrencyCode)
                            {
                                Console.WriteLine("Enter a Valid CurrencyCode");
                            }
                            break;
                        }
                    case StaffOptions.ChargeofOtherAccont:
                        {
                            Console.WriteLine("Enter the new Updated IMPS Chagrges");
                            float chargeImps = Convert.ToSingle(Console.ReadLine());
                            Console.WriteLine("Enter the new Updated RTGS Chagrges");
                            float chargeRtgs = Convert.ToSingle(Console.ReadLine());
                            bankService.UpdateOtherAccountCharges(chargeImps, chargeRtgs, bankId);

                            break;
                        }
                    case StaffOptions.ChargeofSameAccount:
                        {
                            Console.WriteLine("Enter the new Updated IMPS Chagrges");
                            float chargeImps = Convert.ToSingle(Console.ReadLine());
                            Console.WriteLine("Enter the new Updated RTGS Chagrges");
                            float chargeRtgs = Convert.ToSingle(Console.ReadLine());
                            bankService.UpdateSameAccountCharges(chargeImps, chargeRtgs, bankId);

                            break;
                        }
                    case StaffOptions.ViewUserTransactionHistory:

                        {
                            try
                            {
                                Console.WriteLine("Enter AccontId");
                                string accountId = Console.ReadLine();
                                Output.PrintTransactionHistory(bankService,accountId);
                            }
                            catch (InvalidId)
                            {
                                Console.WriteLine("Enter Valid AccountID");
                            }
                            break;
                        }
                    case StaffOptions.Exit:
                        {
                            tryAgain = false;
                            break;
                        }
                    case StaffOptions.RevertTranaction:
                        {
                            Console.WriteLine("Enter a Valid TransactionId");
                            string transactionId = Console.ReadLine();
                            try
                            {
                                bankService.RevertTransaction(transactionId);
                            }
                            catch
                            {
                                Console.WriteLine("Enter a valid Transaction ID");
                            }
                            break;
                        }
                    default:
                        { Console.WriteLine("Enter Valid Options");break; }
                }
            }
        }
    }
}
