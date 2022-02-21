using BankApp.Models;
using System;
using System.Collections.Generic;

namespace BankApp.Services
{
    public interface IBankService
    {
        float AddCurrency(string currencyCode, string bankId);
        string AddStaff(string bankId, string name, string password, Genders gender);
        bool AuthenticateBankStaff(string staffId, string password);
        string CreateBank(string name);
        string CreateAccount(string name, string pin, string phoneNo, string bankId);
        string CreateToken(Account account);
        string CreateToken(string staffId);
        bool DeleteAccount(string accountId,string bankId);
        Account FindAccount(string _accountId);
        Bank FindBank(string bankId);
        string FindBankId(string staffId);


        string GenerateRandomId(string name);
        List<(float amount, string sourceId, string recieverId, int type, DateTime dateTime)> GetTransaction(string accountId);
        bool RevertTransaction(string transactionId);
        void UpdateOtherAccountCharges(float _impsCharge, float _rtgsCharge, string bankId);
        void UpdateSameAccountCharges(float _impsCharge, float _rtgsCharge, string bankId);
    }
}