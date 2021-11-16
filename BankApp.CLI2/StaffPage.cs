using System;
using System.Collections.Generic;
using BankApp.Services;
using BankApp.Models;

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
    }
    public class StaffPage
    {
        public string bankId { get; set; }

        public void Display()
        {
            Console.WriteLine("Chosse from Below Options");
            Console.WriteLine("1.CreateAccount = 1\n2.DeleteAccount\n3.AddCurrency\n4.ChargeofSameAccount\n5.ChargeofOtherAccont\n6.ViewUserTransactionHistory\n7.RevertTranaction");
            StaffOptions choosen = (StaffOptions)Enum.Parse(typeof(StaffOptions), Console.ReadLine());
            bool tryAgain = true;
            while (tryAgain)
            {
                switch (choosen)
                {
                    case StaffOptions.CreateAccount:
                        {
                            try
                            {
                                string name = Console.ReadLine();
                                string pin = Console.ReadLine();
                                string phoneNo = Console.ReadLine();
                                string bankId = Console.ReadLine();
                                string id = AccountService.CreateAccount(name, pin, phoneNo, bankId);
                                Console.WriteLine("Your Account id is {}", id);
                                break;
                            }

                            catch
                            {
                                Console.WriteLine("Enter Valid Details");
                                throw;
                            }
                        }
                    case StaffOptions.DeleteAccount:
                        {
                            
                            Console.WriteLine("Enter the Id of the Account you Want to Delete");
                            string accountId = Console.ReadLine();
                            Bank bank = BankService.FindBank(bankId);
                            Account account = AccountService.FindAccount(accountId, bank);
                            bank.accounts.Remove(account);
                            break;
                        }
                    case StaffOptions.AddCurrency:
                        {
                            Console.WriteLine("Enter the Name of the New Currency");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter its currency code ");
                            string code = Console.ReadLine();
                            code = code.ToUpper();
                            BankService.AddCurrency(name, code, bankId);
                            break;
                        }
                    case StaffOptions.ChargeofOtherAccont:
                        {
                            Console.WriteLine("Enter the new Updated IMPS Chagrges");
                            float chargeImps = Convert.ToSingle(Console.ReadLine());
                            Console.WriteLine("Enter the new Updated RTGS Chagrges");
                            float chargeRtgs = Convert.ToSingle(Console.ReadLine());
                            BankService.UpdateOtherAccountCharges(chargeImps, chargeRtgs, bankId);

                            break;
                        }
                    case StaffOptions.ChargeofSameAccount:
                        {
                            Console.WriteLine("Enter the new Updated IMPS Chagrges");
                            float chargeImps = Convert.ToSingle(Console.ReadLine());
                            Console.WriteLine("Enter the new Updated RTGS Chagrges");
                            float chargeRtgs = Convert.ToSingle(Console.ReadLine());
                            BankService.UpdateSameAccountCharges(chargeImps, chargeRtgs, bankId);

                            break;
                        }
                }
            }
        }
    }
}
