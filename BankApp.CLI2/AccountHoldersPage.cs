using System;
using BankApp.Models.Exceptions;
using BankApp.Services;
using BankApp.Models;


namespace BankApp.CLI2
{
    public enum HoldersOptions
    {
        DepositAmount=1,
        WithdrawAmount,
        TranferFunds,
        ViewTransactionHistory,
        Exit,
    }
    public class  AccountHoldersPage
    {
        public void Display()
        {
            bool tryAgain = true;
           
            while (tryAgain)
            {
                Console.WriteLine("Chose the Below Options:");
                Console.WriteLine("1.Deposit\n2.WIthDrawAmount\n3.TransferFunds\n4. View Transaction History\n 6. Exit");
                HoldersOptions choosen = (HoldersOptions)Enum.Parse(typeof(HoldersOptions), Console.ReadLine());
                
                switch (choosen)
                {
                    case HoldersOptions.DepositAmount:
                        {
                            try
                            {
                                string id;
                                string pin;
                                var details = Capture.CaptureDetails();
                                id = details.Item1;
                                pin = details.Item2;
                                string bankId = details.Item3;
                                Bank bank = BankService.FindBank(bankId);
                                string currencyCode = Capture.CaptureCurrency(bank); 
                                int amount = Capture.CaptureAmount();
                                Console.WriteLine("Your Account Balance is: ");
                                Console.WriteLine(AccountService.Deposit(amount, id, pin, bankId, currencyCode));

                            }
                            catch (InvalidId ex)
                            { Console.WriteLine(ex.Message); }
                            catch (InvalidPin ex)
                            { Console.WriteLine(ex.Message); }
                            
                            catch(InvalidCurrencyCode ex)
                            {
                                Console.WriteLine(ex.Message);
                                
                            }
                            catch
                            {
                                Console.WriteLine("Enter Valid Details");
                                
                            }
                            break;
                        }
                    case HoldersOptions.WithdrawAmount:
                        {
                            try
                            {
                                string id;
                                string pin;
                                var details = Capture.CaptureDetails();
                                id = details.Item1;
                                pin = details.Item2;
                                Console.WriteLine("Enter The Amount in INR only");
                                int amount = Capture.CaptureAmount();
                                string bankId = details.Item3;
                                Console.Write("Your current Balance is ");
                                Console.WriteLine(AccountService.WithDraw(amount, id, pin, bankId));
                                break;
                            }
                            catch (InvalidId ex)
                            { Console.WriteLine(ex.Message); break; }

                            catch (InvalidPin ex)
                            { Console.WriteLine(ex.Message); break; }
                            catch (InvalidBankId ex)
                            { Console.WriteLine(ex.Message); break; }
                            catch (NotEnoughBalance ex)
                            {
                                Console.WriteLine(ex.Message); break;
                            }
                            
                        }
                    case HoldersOptions.TranferFunds:
                        {
                            try
                            {
                                string id;
                                string pin;
                                string bankId;
                                var details = Capture.CaptureDetails();
                                id = details.Item1;
                                pin = details.Item2;
                                bankId = details.Item3;
                                Console.WriteLine("Enter amount to be transffered in INR");
                                float amount = Convert.ToSingle(Console.ReadLine());
                                Console.WriteLine("Enter BankId of Receiver");
                                string rbankId=Console.ReadLine();
                                Console.WriteLine("Enter AccountID of Receiver");
                                string receiverId = Console.ReadLine();
                                TransactionService transactionService = (TransactionService)Enum.Parse(typeof(TransactionService), Console.ReadLine());
                                AccountService.Transfer(id, receiverId, pin, amount, rbankId,bankId,transactionService);
                                Console.WriteLine("Successfully Transferred");
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
                    case HoldersOptions.ViewTransactionHistory:
                        {
                            //try
                            //{
                                string id;
                                string bankId;
                                string pin;
                                var details = Capture.CaptureDetails();
                                id = details.Item1;
                                pin = details.Item2;
                                bankId = details.Item3;
                                
                                Output.PrintTransactionHistory(id,bankId,pin);


                                break;
                            //}
                            /*catch
                            {
                                Console.WriteLine("Enter Valid Details");
                                break;
                            }*/
                        }
                    case HoldersOptions.Exit:
                        {
                            tryAgain = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Enter a Valid Choice");
                            break;
                        }
                }
            }
        }
            
    }
}
