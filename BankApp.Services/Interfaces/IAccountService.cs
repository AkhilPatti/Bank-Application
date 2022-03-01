using BankApp.Models;
using System;
using System.Collections.Generic;

namespace BankApp.Services
{
    public interface IAccountService
    {
        bool AccountValidator(string accountId, string pin);
        bool CheckCurrencyExist(string bankId, string currencyCode);
        float ConvertToRupees(string currencyCode, float amount, string bank);
        string CreateAccount(string name, string newPin, string phoneNo, string bankId);
        string CreateToken(Account account);
        float Deposit(float amount, string accountId, string pin, string currencyCode);
        Account FindAccount(string id);
        Transaction FindTransaction(string transactionId);
        List<string> GetCurrencies(string bankId);

        List<(float, string, string, int, DateTime,string)> GetTransaction(string accountId);
        string Transfer(string senderId, string receiverId, string senderPin, float amount, TransactionService transactionService);
        float Withdrawl(float amount, string id, string pin);
    }
}