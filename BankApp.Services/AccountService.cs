using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BankApp.Models;
using BankApp.Models.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace BankApp.Services
{
    public class AccountService : IAccountService
    {

        BankDbContext db;
        private IConfiguration configuration;
        public AccountService(BankDbContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;

        }

        private Bank FindBank(string bankId)
        {
            try
            {
                return db.Banks.Single(m => m.bankId == bankId);
            }
            catch
            {
                throw new InvalidBankId();
            }
        }
        public string CreateAccount(string name, string newPin, string phoneNo, string bankId)
        {
            string accountId = name.Substring(0, 3) + bankId.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyyHHmmss");
            db.Add(new Account
            {
                accountHolderName = name,
                pinHash = BCrypt.Net.BCrypt.HashPassword(newPin),
                phoneNumber = phoneNo,
                balance = 0,
                bankId = bankId,
                accountId = accountId,
            });
            db.SaveChanges();
            return accountId;
        }
        public string CreateToken(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,"user")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
            (
                claims: claims,
                signingCredentials: cred,
                expires: DateTime.Now.AddDays(1));
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        public bool AccountValidator(string accountId, string pin)
        {
                var account = FindAccount(accountId);
                if (account == null)
                {

                    throw new InvalidId();

                }

                if (BCrypt.Net.BCrypt.Verify(pin, account.pinHash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        /*private bool AccountExists(string id)
        {
            try
            {

                var Account = db.Accounts.Single(m => m.accountId == id);
                return true;
            }
            catch
            {
                return false;
            }
        }*/

        public Account FindAccount(string id)
        {
            Account account = db.Accounts.SingleOrDefault(m => String.Equals(m.accountId, id));

            if (account!=null)
            {
                return account;
            }
            else
            {
                throw new InvalidId();
            }
        }
        public float Deposit(float amount, string accountId, string pin, string currencyCode)
        {

            if (!AccountValidator(accountId, pin))
            {
                throw new InvalidPin();
            }
            else
            {
                var account = FindAccount(accountId);
                account.balance += amount;
                db.Update(account);
                string transactionId = GenerateTransaction(null, accountId, amount, TransactionType.WithDrawl, GenerateTransId(DateTime.Now.ToString("ddMMyyyyHHmmss"), accountId, account.bankId));
                db.SaveChangesAsync();
                return account.balance;
            }

        }
        public float ConvertToRupees(string currencyCode, float amount, string bankId)
        {
            if (CheckCurrencyExist(bankId, currencyCode))
            {
                var currency = db.Currencies.Single(m => String.Equals(m.currencyCode, currencyCode));
                float newAmount = amount * (float)currency.exchangeRate;
                Console.WriteLine(currency.exchangeRate);
                return newAmount;
            }
            else
            {
                throw new InvalidCurrencyCode();
            }
        }
        public float WithDraw(float amount, string id, string pin)
        {
            if (AccountValidator(id, pin))
            {

                Account account = FindAccount(id);
                float balance = account.balance;
                if (balance < amount)
                {
                    throw new NotEnoughBalance();
                }
                else
                {
                    account.balance -= amount;
                    string transactionId =GenerateTransaction(id,null,amount,TransactionType.WithDrawl,GenerateTransId(DateTime.Now.ToString("ddMMyyyyHHmmss"), id,account.bankId));
                    db.Update(account);
                    db.SaveChangesAsync();
                    return account.balance;
                }
            }
            else { throw new InvalidPin(); }
        }

        public string Transfer(string senderId, string receiverId, string senderPin, float amount, TransactionService transactionService)
        {
            

            if (AccountValidator(senderId, senderPin))
            {
                Account saccount = FindAccount(senderId);
                Account raccount = db.Accounts.SingleOrDefault(m => m.accountId == receiverId);
                if (raccount!=null)
                {
                    Bank sbank = FindBank(saccount.bankId);
                    if (saccount.balance < amount)
                    { throw new NotEnoughBalance(); }
                    double charge;
                    if ((int)transactionService == 1)
                    {

                        if (string.Equals(raccount.bankId, saccount.bankId))
                        { charge = sbank.sImps; }
                        else
                        {
                            charge = sbank.oImps;
                        }
                    }

                    else
                    {

                        if (string.Equals(raccount.bankId, saccount.bankId)) { charge = sbank.sRtgs; }
                        else
                        {
                            charge = sbank.oRtgs;
                        }
                    }
                    raccount.balance += (float)(amount - (amount * charge));
                    saccount.balance -=amount;
                    Transaction transaction = new Transaction
                    {
                        sourceAccountId = senderId,
                        receiveraccountId = receiverId,
                        amount = amount,
                        type = TransactionType.Transfer,
                        on = DateTime.Now,
                        transactionId = GenerateTransId(DateTime.Now.ToString("ddMMyyyyHHmmss"), senderId, saccount.bankId),

                    };
                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    return transaction.transactionId;
                }
                else
                {
                    throw new InvalidReceiver();
                }

            }
            else
            {
                throw new InvalidReceiver();
            }
        }
        private string GenerateTransaction(string senderId,string receiverId, float amount ,TransactionType type,string transactionId)
        {
            Transaction transaction = new Transaction
            {
                sourceAccountId = (int)type!=1?senderId:null,
                receiveraccountId = (int)type!=2?receiverId:null,
                amount = amount,
                type = type,
                on = DateTime.Now,
                transactionId = transactionId

            };
            db.Transactions.Add(transaction);
            db.SaveChanges();
            return transaction.transactionId;
        }

        public List<string> GetCurrencies(string bankId)
        {
            List<string> currencies = new List<string>();
            var bankcurrencies = db.BankCurrencies.Where(m => m.bankId == bankId).ToList();
            foreach (var item in bankcurrencies)
            {
                currencies.Add(item.currerncyCode);
            }
            return currencies;
        }
        public bool CheckCurrencyExist(string bankId, string currencyCode)
        {
            try
            {
                var currency = db.BankCurrencies.Single(m => (String.Equals(bankId, m.bankId) && (String.Equals(currencyCode, m.currerncyCode))));
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal string GenerateTransId(string date, string AccountId, string bankId)
        {
            return "TXN" + bankId + AccountId + date;
        }
        public Transaction FindTransaction(string transactionId)
        {
            return db.Transactions.FirstOrDefault(m => m.transactionId== transactionId);
        }
        public List<(float, string, string, int, DateTime,string)> GetTransaction(string accountId)
        {
            List<(float, string, string, int, DateTime,string)> transactions = new List<(float, string, string, int, DateTime,string)>();
            var transaction = db.Transactions.Where(m => ((m.receiveraccountId == accountId) || (m.sourceAccountId == accountId))).ToList();
            foreach (var item in transaction)
            {
                transactions.Add((item.amount, item.sourceAccountId, item.receiveraccountId, (int)item.type, item.on,item.transactionId));
            }
            return transactions;
        }

    }

}