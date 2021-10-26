using System;
using BankApp.Services;
using BankApp.Models.Exceptions;
using System.Collections.Generic;
using BankApp.Models;

namespace BankAppCLI2
{
    public enum UserOptions
    {
        CreateAccount = 1,
        Deposit,
        WithDraw,
        Transfer,
        TransactionHistory,
        Exit,
    }
    class Program
    {
        static void Main(string[] args)
        {


            bool TryAgain = true;
            while (TryAgain)
            {
                UserOptions userOption = (UserOptions)Enum.Parse(typeof(UserOptions), Console.ReadLine());
                switch (userOption)
                {
                    case UserOptions.CreateAccount:
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
                    case UserOptions.Deposit:
                        {
                            try
                            {
                                string id;
                                string pin;
                                var details = CaptureDetails();
                                id = details.Item1;
                                pin = details.Item2;
                                int amount = CaptureAmount();
                                string bankId = Console.ReadLine();
                                AccountService.Deposit(amount, id, pin, bankId);

                            }
                            catch (InvalidId ex)
                            { Console.WriteLine(ex.Message); }
                            catch (InvalidPin ex)
                            { Console.WriteLine(ex.Message); }
                            catch
                            {
                                Console.WriteLine("Enter Valid Details");
                                throw;
                            }
                            break;
                        }
                    case UserOptions.WithDraw:
                        {
                            //capturing Sender's Details
                            try
                            {
                                string id;
                                string pin;
                                var details = CaptureDetails();
                                id = details.Item1;
                                pin = details.Item2;
                                int amount = CaptureAmount();
                                string bankId = Console.ReadLine();
                                AccountService.WithDraw(amount, id, pin, bankId);
                                break;
                            }
                            catch (InvalidId ex)
                            { Console.WriteLine(ex.Message); break; }

                            catch (InvalidPin ex)
                            { Console.WriteLine(ex.Message); break; }

                            catch
                            {
                                Console.WriteLine("Enter Valid Details");
                                break;
                            }

                        }
                    case UserOptions.Transfer:
                        {
                            try
                            {
                                string id;
                                string pin;
                                var details = CaptureDetails();
                                id = details.Item1;
                                pin = details.Item2;
                                int receiverId = Convert.ToInt32(Console.ReadLine());
                                break;
                            }
                            catch (InvalidId ex)
                            { Console.WriteLine(ex.Message); break; }

                            catch (InvalidPin ex)
                            { Console.WriteLine(ex.Message); break; }

                            catch (InvalidReceiver ex)
                            { Console.WriteLine(ex.Message); break; }

                            catch (NotEnoughBalance ex)
                            { Console.Write(ex.Message); break; }

                            catch
                            {
                                Console.WriteLine("Enter Valid Details");

                                break;
                            }

                        }

                     case UserOptions.TransactionHistory:
                        {
                            try
                            {
                                string id = Console.ReadLine();
                                string bankId = Console.ReadLine();
                                PrintTransactionHistory(id,bankId);


                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Enter Valid Details");
                                break;
                            }
                        }
                    case UserOptions.Exit:
                        {
                            TryAgain = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Enter a Valid Option");
                            break;
                        }


                }
            }

            void PrintTransactionHistory(string id, string BankId)
            {
                var transList = new List<Tuple<string, string, TransactionType, DateTime, int>>();

                foreach (Tuple < string, string, TransactionType, DateTime, int> items in transList)
                {
                    if (items.Item3 == TransactionType.Credit)
                    {
                        if (string.IsNullOrEmpty(items.Item1))
                        {
                            Console.WriteLine("{} amount is withdrawn on ", items.Item5, items.Item2);
                        }
                        else
                        {
                            Console.WriteLine("{} amount is debbited into {} account from {} On {items.Item4}", items.Item5,items.Item2, items.Item1);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(items.Item2))
                        {
                            Console.WriteLine("{} amount is deposited  on ", items.Item5, items.Item2);
                        }
                        else
                        {
                            Console.WriteLine("{} amount is debitted from {} account from {} On {items.Item4}", items.Item5,items.Item2, items.Item1);
                        }
                    }
                }
            }
            //Function for Capturing Detais (i.e id,pin);
            static Tuple<string, string> CaptureDetails()
            {
                string id;
                string pin;
                Console.Write("Enter your AccountId : ");
                id = Console.ReadLine();
                Console.Write("Enter your Account Pin : ");
                pin = Console.ReadLine();
                return Tuple.Create(id, pin); ;
            }
            static int CaptureAmount()
            {
                Console.WriteLine("Enter the Amount");
                int amount = Convert.ToInt32(Console.ReadLine());
                return amount;
            }
        }
    }
}