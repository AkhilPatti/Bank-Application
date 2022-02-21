using System;
using System.Collections.Generic;
using System.Linq;
using BankApp.Models.Exceptions;
using BankApp.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace BankApp.Services
{
    public class BankService : IBankService
    {
        //#pragma warning disable 0649

        BankDbContext db;
        IConfiguration configuration;
        public BankService(IConfiguration _configuration, BankDbContext _bankDbContext)
        {
            configuration = _configuration;
            db = _bankDbContext;

        }


        public string CreateBank(string name)
        {

            Bank bank = new Bank()
            {
                bankName = name,
                sImps = 0,
                sRtgs = 0,
                oImps = 0,

                bankId = GenerateRandomId(name),
                oRtgs = 0
            };
            db.Banks.Add(bank);

            return bank.bankId;
        }
        internal bool BankExists(string id)
        {
            try
            {
                var bank = db.Banks.Single(m => m.bankId == id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GenerateRandomId(string name)
        {
            name = name.ToUpper();
            string id = name.Substring(0, 3);
            string date = DateTime.Now.ToString("ddMMyyyyHHmmss");
            id = id + date;
            return id;
        }

        public Bank FindBank(string bankId)
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
        public string CreateAccount(string name, string pin, string phoneNo, string bankId)
        {
            string accountId = name.Substring(0, 3) + bankId.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyyHHmmss");
            db.Add(new Account
            {
                accountHolderName = name,
                pinHash = BCrypt.Net.BCrypt.HashPassword(pin),
                phoneNumber = phoneNo,
                balance = 0,
                bankId = bankId,
                accountId = accountId,
            });
            db.SaveChanges();
            return accountId;
        }

        public bool DeleteAccount(string accountId, string bankId)
        {
            try
            {
                var account = db.Accounts.Single(m => m.accountId == accountId);
                if (account.bankId == bankId)
                {
                    db.Accounts.Remove(account);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    throw new InvalidBankId();
                }
            }
            catch(InvalidBankId)
            {
                throw new InvalidBankId();
            }
            catch
            {
                throw new AccountNotFound();
            }
        }
        public float AddCurrency(string currencyCode, string bankId)
        {

            try
            {
                var currency = db.Currencies.Single(m => m.currencyCode == currencyCode);
                float exchangeRate = (float)currency.exchangeRate;
                db.BankCurrencies.Add(new BankCurrencies
                {
                    bankId = bankId,
                    currerncyCode = currencyCode
                });
                db.SaveChanges();
                return (float)exchangeRate;
            }
            catch
            {
                throw new InvalidCurrencyCode();
            }
        }

        public void UpdateSameAccountCharges(float _impsCharge, float _rtgsCharge, string bankId)
        {
            Bank bank = FindBank(bankId);
            bank.sImps = _impsCharge;
            bank.sRtgs = _rtgsCharge;
            db.Banks.Update(bank);
            db.SaveChanges();
        }

        public void UpdateOtherAccountCharges(float _impsCharge, float _rtgsCharge, string bankId)
        {
            Bank bank = FindBank(bankId);
            bank.oImps = _impsCharge;
            bank.oRtgs = _rtgsCharge;
            db.Banks.Update(bank);
            db.SaveChanges();
        }

        public Account FindAccount(string _accountId)
        {
            try
            {
                Account account = db.Accounts.Single(m => m.accountId == _accountId);
                return account;
            }
            catch
            {
                throw new InvalidId();
            }
        }
        public string AddStaff(string bankId, string name, string password, Genders gender)
        {
            BankStaff staff = new()
            {
                staffName = name,
                password = password,
                bankId = bankId,
                staffId = name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyyHHmmss")
            };
            db.Staff.Add(staff);
            return staff.staffId;
        }
        public string FindBankId(string staffId)
        { 
            var bank = db.Staff.FirstOrDefault(i => i.staffId == staffId);
                return bank.bankId;
        }
        public string CreateToken(string staffId)
        {
            var bankId = FindBankId(staffId);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,"staff"),
                new Claim(ClaimTypes.Name,staffId),
                new Claim("bankId",bankId)
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
        public string CreateToken(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,"User")
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

        public bool AuthenticateBankStaff(string staffId, string password)
        {

                var staff = db.Staff.SingleOrDefault(m => m.staffId == staffId);
                if (staff == null)
                {

                    throw new InvalidStaff();

                }
            ;
                if (BCrypt.Net.BCrypt.Verify(password, staff.password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public bool RevertTransaction(string transactionId)
        {

            Transaction transaction;
            try
            {
                transaction = db.Transactions.Single(m => m.transactionId == transactionId);
            }
            catch
            {
                throw new InvalidTransactionId();
            }

            switch ((int)transaction.type)
            {
                case 0:
                    {
                        try
                        {
                            var account = FindAccount(transaction.sourceAccountId);
                            account.balance += transaction.amount;
                            db.Accounts.Update(account);
                            db.SaveChanges();
                            return true;
                        }
                        catch
                        {
                            throw new InvalidId();
                        }

                    }
                case 2:
                    {
                        try
                        {
                            var account = FindAccount(transaction.receiveraccountId);

                            if (transaction.amount >= account.balance)
                            {
                                account.balance = transaction.amount;
                                db.Accounts.Update(account);
                                db.SaveChanges();
                                return true;
                            }
                            else { return false; }
                        }
                        catch
                        {
                            throw new InvalidReceiver();
                        }

                    }
                case 3:
                    {
                        Account saccount;
                        try
                        {
                            saccount = FindAccount(transaction.sourceAccountId);
                        }
                        catch
                        {
                            throw new InvalidId();
                        }
                        Account raccount;
                        try { raccount = FindAccount(transaction.receiveraccountId); }
                        catch { throw new InvalidReceiver(); };
                        if (raccount.balance < transaction.amount)
                        {
                            return false;
                        }
                        raccount.balance -= transaction.amount;
                        saccount.balance += transaction.amount;
                        db.Accounts.Update(raccount);
                        db.Accounts.Update(saccount);
                        break;
                    }
            }
            return true;
        }

        public List<(float amount, string sourceId, string recieverId, int type, DateTime dateTime)> GetTransaction(string accountId)
        {
            List<(float, string, string, int, DateTime)> transactions = new List<(float, string, string, int, DateTime)>();
            var transaction = db.Transactions.Where(m => (m.receiveraccountId == accountId || m.sourceAccountId == accountId && m.type == TransactionType.Transfer))
                                .Select(m => new
                                {
                                    sourceAccountId = m.sourceAccountId ?? "",
                                    amount = m.amount,
                                    receiveraccountId = m.receiveraccountId ?? "",
                                    type = m.type == TransactionType.Deposit ? 0 : m.type == TransactionType.Transfer ? 1 : 2,
                                    on = m.on
                                });
            foreach (var item in transaction)
            {
                transactions.Add((item.amount, item.sourceAccountId, item.receiveraccountId, (int)item.type, item.on));
            }
            return transactions;

        }
    }
}