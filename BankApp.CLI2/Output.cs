using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp.Models;
using BankApp.Services;
namespace BankApp.CLI2
{
    public static class Output
    {
        public static void PrintTransactionHistory(AccountService accountService ,string id ,string pin)
        { 
            if (accountService.AccountValidator(id, pin))
            {
                List<(float amount, string sourceId, string receiverId,int type,DateTime dateTime)>transList = accountService.GetTransaction(id);
                
                foreach ((float ,string,string,int,DateTime)items in transList)
                {
                    if (items.Item4 == (int)TransactionType.WithDrawl)
                    {
                        if (string.IsNullOrEmpty(items.Item2))
                        {
                            Console.WriteLine("{0} amount is withdrawn on {1}", items.Item1, items.Item5.ToString("ddMMyyyyyHHmmss"));
                        }
                    }
                    else if (items.Item4 == (int)TransactionType.Transfer)
                    {
                        Console.WriteLine("{0} amount is debbited into {1} account from {2} On {3}", items.Item1, items.Item2, items.Item3, items.Item5);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(items.Item2))
                        {
                            Console.WriteLine("{0} amount is deposited  on {1}", items.Item1, items.Item5.ToString("D HH:mm"));
                        }
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("ENter Valid Details");
            }
        }


        public static void PrintTransactionHistory(BankService bankService,string id)
        {
         

                List<(float amount, string sourceId, string receiverId, int type, DateTime dateTime)> transList = bankService.GetTransaction(id);
            
                foreach ((float, string, string, int, DateTime) items in transList)
                {
                
                    if (items.Item4 == (int)TransactionType.WithDrawl)
                    {
                        if (string.IsNullOrEmpty(items.Item2))
                        {
                            Console.WriteLine("{0} amount is withdrawn on {1}", items.Item1, items.Item5.ToString("ddMMyyyyyHHmmss"));
                        }
                    }
                    else if (items.Item4 == (int)TransactionType.Transfer)
                    {
                        Console.WriteLine("{0} amount is debbited into {1} account from {2} On {3}", items.Item1, items.Item2, items.Item3,items.Item5);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(items.Item2))
                        {
                            Console.WriteLine("{0} amount is deposited  on {1}", items.Item1, items.Item5.ToString("D HH:mm"));
                        }

                    }
                }
            }

        }

        
    }

