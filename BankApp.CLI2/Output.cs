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
        public static void PrintTransactionHistory(string id, string bankId,string pin)
        {

            Bank bank = BankService.FindBank(bankId);
            Account account = AccountService.FindAccount(id, bank);
            List<Transaction>  transList = account.transactions;
            if (AccountService.AccountValidator(id, pin, account))
            {
                foreach (Transaction items in transList)
                {
                    if (items.type == TransactionType.Credit)
                    {
                        if (string.IsNullOrEmpty(items.accountId))
                        {
                            Console.WriteLine("{} amount is withdrawn on ", items.amount, items.on);
                        }
                        else
                        {
                            Console.WriteLine("{} amount is debbited into {} account from {} On {items.Item4}", items.amount, items.sourceAccountId, items.accountId);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(items.sourceAccountId))
                        {
                            Console.WriteLine("{0} amount is deposited  on {1}", items.amount, items.on.ToString("D HH:mm"));
                        }
                        else
                        {

                            Console.WriteLine("{0} amount is debitted from {1} account to {2} On {3}", items.amount, items.accountId, items.sourceAccountId, items.on.ToString("D HH:mm"));
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("ENter Valid Details");
            }
        }


        public static void PrintTransactionHistory(string id, string bankId)
        {
            try
            {

                Bank bank = BankService.FindBank(bankId);
                Account account = AccountService.FindAccount(id, bank);
                List<Transaction> transList = account.transactions;
        
                foreach (Transaction items in transList)
                {
                    if (items.type == TransactionType.Credit)
                    {
                        if (string.IsNullOrEmpty(items.accountId))
                        {
                            Console.WriteLine("{} amount is withdrawn on ", items.amount, items.on);
                        }
                        else
                        {
                            Console.WriteLine("{} amount is debbited into {} account from {} On {items.Item4}", items.amount, items.sourceAccountId, items.accountId);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(items.sourceAccountId))
                        {
                            Console.WriteLine("{} amount is deposited  on ", items.amount, items.on);
                        }
                        else
                        {
                            Console.WriteLine("{} amount is debitted from {} account to {} On {}", items.amount, items.accountId, items.sourceAccountId, items.on);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Enter Valid Details");
            }


        }
    }
}
