using System;
using System.Collections.Generic;
using BankApp.Services;
using BankApp.Models;
using BankApp.Services.Exceptions;
using System.Diagnostics;

namespace BankApp.CLI
{

    class Program
    {
        static void Main(string[] args)
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

        bool TryAgain = true;
        while(TryAgain)
        {
        UserOptions userOption = (UserOptions)Enum.Parse(typeof(UserOptions), Console.Readline());
        switch(userOption)
            {
            case UserOptions.CreateAccount:
                        {
                            try
                                {
                                    
                                    AccountService.CreateAccount();
                                    break;
                                }
                            
                            
                            catch {
                                     Console.WriteLine("Enter Valid Details");
                                     throw;
                                  }
                            
                        }                
            case UserOptions.Deposit:
                        {
                            try
                            {
                                int id;
                                string pin;
                                id,pin = CaptureDetails();
                                AccountService.Deposit(id, pin);
                               
                            }
                            catch (InvalidId ex)
                            { Console.WriteLine(ex.Message); }
                            catch (InvalidPin ex)
                            { Console.WriteLine(ex.Message); }
                            catch()
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
                                    int id;
                                    string pin;
                                    id,pin = CaptureDetails();
                                    AccountService.WithDraw(id, pin);
                                    break;
                                }
                                catch (InvalidId ex)
                                { Console.WriteLine(ex.Message); break; }

                                catch (InvalidPin ex)
                                { Console.WriteLine(ex.Message); break; }

                                catch ()
                                {
                                    Console.WriteLine("Enter Valid Details");
                                    throw;
                                    break;
                                }
                                
                            }
            case UserOptions.Transfer:
                    {
                    try
                    {
                        int id;
                        string pin;
                        id,pin = CaptureDetails();
                        int receiverId = Convert.ToInt32(Console.ReadLine())
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

                    catch ()
                    {
                        Console.WriteLine("Enter Valid Details");
                        throw;
                        break;
                    }
                }

            case UserOptions.TranactionHistory:
                        {
                            try()
                            {
                                int id = Covert.ToInt32(Console.ReadLine());
                                PrintTranactionHistory(id);
                                break;
                            }
                            catch()
                            {
                               Console.WriteLine("Enter Valid Details");
                            }
                        }
             case UserOptions.Exit:
                        {
                            TryAgain = false;
                            break;
                        }
            default:
                        Console.WriteLine("Enter a Valid Option");
        }
 
       
}
}

void PrintTransactionHistory(int id)
{
    foreach(Tuple<int,int,int> items in accounts[id].transactionHistory)
    {
        if( items.Item1 ==id)
        {
            if (items.Item2 < 0)
            {
                Console.WriteLine("{} amount is withdrawn\nTherefore your Account Balance is {}", items.Item2,items.Item3);
            }
            else
            {
                Console.WriteLine("{} amount is added to your Account\nTherefore your Account Balance is {}", items.Item2, items.Item3);
            }
        }
        else
        {
            Console.WriteLine("{} amount is tranferred to {} \nTherefore your Account Balance is {}", items.Item2,items.Item3);
        }
    }
}
//Function for Capturing Detais (i.e id,pin);
Tuple <int,int> CaptureDetails()
{   int id;
    string pin;
    Console.Write("Enter your AccountId : ");
    id = Convert.ToInt32(Console.ReadLine());
    Console.Write("Enter your Account Pin : ");
    pin = Console.ReadLine();
    return new Tuple(int id, string pin);
}
}