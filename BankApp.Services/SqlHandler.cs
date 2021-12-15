using System;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;
using BankApp.Models;
using System.Globalization;
using BankApp.Models.Exceptions;


namespace BankApp.Services
{
    internal class SqlHandler

    {

         string connStr;
        public void StartConnection()
        {
            //Server=localhost;Database=My_Mysql_Database;Uid=root;Pwd=root;
            connStr = "server=localhost;Uid=root;database=BankDataBase;port=3306;Pwd=Radha@65";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CheckTabelsExist, conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        string read = "";
                        while (reader.Read())
                        {
                            read += reader.GetString(0);
                        }
                        if (read != "1" || read == null)
                            CreateTables();

                    }

                }
            }
            catch
            {
                throw new ErrorInitializingDB();
            }
        }

        public void CreateTables()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CreateBanks, conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                }
            }

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CreateTransactions, conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                }
            }

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CreateCurrencies, conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                }
            }

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CreateStaffAccounts, conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                }
            }

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(SqlQueries.CreateBankCurrecies, conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                }
            }


        }

        public string AddUser(string _name,string phoneNo, string password, string bankId)
        {
            
            
            string accountId = _name.Substring(0, 3) + bankId.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyyHHmmss");
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AddUser,accountId,_name,password,phoneNo,bankId), conn))
                    {
                    
                        cmd.Connection.Open();
                        MySqlDataReader reaer = cmd.ExecuteReader();
                    }
                }

            }
            catch
            {
                return "Error Occured";
            }
            return accountId;
        }


        public List<string> DisplayCurrencies(string bankId)
        {
            
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetCurrencies,bankId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    List<string> currencyCodes = new List<string>();
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(0));
                        currencyCodes.Add(reader.GetString(0));
                    }
                    return currencyCodes;
                }

            }


        }

        public bool DeleteUser(string accountId)
        {

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.DeleteUser, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }
                }

            }
            catch
            {
                return false;
            }
            return true;
        }

        public string AddStaff(string name, string password, string bankId)
        {
            string staffId = name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyyHHmmss");
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AddStaff, staffId, name, password, bankId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception value)
            {
                return value.ToString();
            }
            return staffId;
        }
        public string AddBank(string name)
        {
            string bankId = name.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyyyHHmmss");
            if (CheckBankExists(bankId))
            {
                throw new BankAlreadyExists();
            }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AddBank, bankId, name), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }
                }
            }
            catch (Exception)
            {
                throw new BankAlreadyExists();
            }
            return bankId;

        }
        public float GetBalance(string accountId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetBalance, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();
                        string store = "";
                        while (srd.Read())
                        {
                            store += srd["Balance"];
                        }
                        return Convert.ToSingle(store);
                    }
                }
            }
            catch
            {
                throw new InvalidId();
            }


        }
        public bool UpdateBalance(string accountId, float balance)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdateBalance, balance, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatesIMPS(float sIMPS, string bankId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdatesIMPS, Convert.ToDouble(sIMPS), bankId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateoIMPS(float oIMPS, string bankId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdateoIMPS, Convert.ToDouble(oIMPS), bankId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();
                        
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatesRTGS(float sRTGS, string bankId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdatesRTGS, Convert.ToDouble(sRTGS), bankId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateoRTGS(float oRTGS, string bankId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdateoRTGS, Convert.ToDouble(oRTGS), bankId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public float GetsIMPS(string bankId)
        {
            
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetsIMPSCharges, bankId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader srd = cmd.ExecuteReader();
                    string store = "";
                    if (srd.Read())
                    {
                        store += srd.GetValue(0);
                    }
                    Console.WriteLine("{0} kio",store);
                    return Convert.ToSingle(store);
                }
            }
        }

        public float GetoIMPS(string bankId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetoIMPSCharges, bankId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader srd = cmd.ExecuteReader();
                    string store = "";
                    while (srd.Read())
                    {
                        store += srd.GetString(0);
                    }
                    return Convert.ToSingle(store);
                }
            }
        }
        public float GetsRTGS(string bankId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetsRTGSCharges, bankId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader srd = cmd.ExecuteReader();
                    string store = "";
                    while (srd.Read())
                    {
                        store += srd.GetString(0);
                    }
                    return Convert.ToSingle(store);
                }
            }
        }
        public float GetoRTGS(string bankId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetoRTGSCharges, bankId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader srd = cmd.ExecuteReader();
                    string store = "";
                    while (srd.Read())
                    {
                        store += srd.GetString(0);
                    }
                    return Convert.ToSingle(store);
                }
            }
        }
        public string GetBankId(string accountId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetUserBankId, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();
                        string store = "";
                        while (srd.Read())
                        {
                            store += srd.GetString(0);
                        }
                        return store;
                    }
                }
            }
            catch (Exception)
            {
                return "BankId not Found";
            }
        }

        public float GetCurrencyRate(string code)
        {
            //try
            //{
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetExchangeRate, code), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        string store = "";
                        while (reader.Read())
                        {
                            store += reader.GetValue(0);
                            
                        }
                        
                        return Convert.ToSingle(store);
                    }
                }
            //}
            /*catch
            {
                return -1.0f;
            }*/
        }

        
        public void GenerateTransaction(string receiverId, string senderId, float amount, int type)
        {
            string transId;
            if (String.Equals(receiverId, ""))
            {
                transId = "TXN" + GetBankId(senderId) + senderId + DateTime.Now.ToString("ddMMyyyyHHmmss");
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AddWithdrawlTransaction, transId, amount, senderId, type, DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }
                }

            }
            else if (String.Equals(senderId, ""))
            {
                transId = "TXN" + GetBankId(receiverId) + receiverId + DateTime.Now.ToString("ddMMyyyyHHmmss");
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AddDepositTransaction, transId, amount, receiverId, type, DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }
                }
            }
            else
            {
                transId = "TXN" + GetBankId(receiverId) + receiverId + DateTime.Now.ToString("ddMMyyyyHHmmss");
                
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AddTransaction, transId, amount, receiverId, senderId, type, DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss")), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                    }
                }
            }
        }
        public float DepositAmount(string accountId, float amount, string code)
        {
            try
            {
                float balance = GetBalance(accountId);
                Console.WriteLine(GetCurrencyRate(code));
                balance = balance + amount * GetCurrencyRate(code);
                Console.WriteLine(balance);
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdateBalance, balance, accountId), conn))
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        GenerateTransaction(accountId,"", amount, 2);
                        return GetBalance(accountId);
                    }
                }
            }
            catch
            {
                return -100.0f;
            }
        }

        public string WithdrawAmount(string accountId, float amount)
        {
            try
            {
                float balance = GetBalance(accountId);
                balance -= amount;
                
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.UpdateBalance, balance, accountId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();
                        GenerateTransaction("", accountId, amount, 1);
                        return "Succesfully Withdrawn Amount";
                    }
                }
            }
            catch (Exception)
            {
                return "Enter Valid Details";
            }
        }
        public string TransferAmount(string sourceId, string recieverId, float amount, string type)
        {
            try
            {
                string sourceBankId = GetBankId(sourceId);
                Console.WriteLine(sourceBankId);
                string recieverBankId = GetBankId(recieverId);
                Console.WriteLine(recieverBankId);
                float charge = 0;
                if (String.Equals(type, "IMPS"))
                {
                    
                    if (String.Equals(sourceBankId, recieverBankId))
                    {
                        
                        charge = GetsIMPS(sourceBankId);
                    }
                    else
                    {
                        charge = GetoIMPS(sourceBankId);
                    }
                }
                else
                {
                    
                    if (String.Equals(sourceBankId, recieverBankId))
                    {
                        charge = GetsRTGS(sourceBankId);
                    }
                    else
                    {
                        charge = GetoRTGS(sourceBankId);
                    }
                }
                
                float sBalance = GetBalance(sourceId);
                if (sBalance<amount)
                {
                    
                    throw new NotEnoughBalance();
                }
                amount = amount - amount * charge / 100;
                GenerateTransaction(sourceId, recieverId, amount, 3);
                
                UpdateBalance(sourceId, GetBalance(sourceId) - amount);
                
                UpdateBalance(recieverId, GetBalance(recieverId) + amount);
                Console.WriteLine("dasdas");
                return $"Successfully transferred amount {amount} from {sourceId} to {recieverId}";
            }
            catch
            {
                return "Enter Valid Details";
            }
        }

        public bool RevertTransaction(string transactionId)
        {
            try
            {
                float amount;
                string receiverId;
                float receiverBalance;
                string senderId;
                int type;
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetTrasnaction, transactionId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();
                        if (srd.Read())
                        {
                            amount = (float)srd["Amount"];
                            receiverId = srd.GetValue(2).ToString();
                            senderId = srd.GetValue(3).ToString();
                            type = int.Parse(srd.GetValue(4).ToString());
                        }
                        else { return false; }

                        receiverBalance = GetBalance(receiverId);


                        switch (type)
                        {
                            case 1:
                                {
                                    UpdateBalance(receiverId, receiverBalance + amount);
                                    break;
                                }
                            case 2:
                                {
                                    if (receiverBalance >= amount)
                                    {
                                        UpdateBalance(receiverId, receiverBalance - amount);
                                    }
                                    else { return false; }
                                    break;
                                }
                            case 3:
                                {
                                    float senderBalance = GetBalance(senderId);
                                    if (receiverBalance < amount)
                                    {
                                        return false;
                                    }
                                    UpdateBalance(senderId, senderBalance + amount);
                                    UpdateBalance(receiverId, receiverBalance - amount);
                                    break;
                                }
                        }


                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool CheckAcceptedCurrency(String bankId, String currencyCode)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.CheckAcceptedAurrency, bankId, currencyCode), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader srd = cmd.ExecuteReader();
                    if (srd.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public bool CheckCurrencyExists(string currencyCode)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.CheckCurrencyExist, currencyCode), conn))

                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (Convert.ToInt32(reader.GetValue(0)) > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        }

        public bool AddCurrency(string currencyCode, string name)
        {
            if (CheckCurrencyExists(currencyCode))
            {
                return false;
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AddCurrency, currencyCode.ToLower()), conn))
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            return true;
        }
        public float AddAcceptedCurrency(string currencyCode, string bankId)
        {
            if (CheckAcceptedCurrency(bankId, currencyCode))
            {
                throw new InvalidCurrencyCode();
            }
            if (!CheckCurrencyExists(currencyCode))
            {
                throw new InvalidCurrencyCode();
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AddAcceptedCurrency, bankId, currencyCode), conn))
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                    }

                }

            }
            return GetCurrencyRate(currencyCode);

        }

        public bool AuthenticateUserExists(string accountId, string password)
        {
            //try
            //{
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AuthenticateCustomerExists, accountId, password), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();
                        while (srd.Read())
                        {
                            
                            
                            if (Convert.ToInt32(srd.GetValue(0)) == 1)
                                return true;
                        }
                        
                        return false;
                    }
                }
            /*}
            catch
            {
                return false;
            }*/
        }

        public bool CheckUserExists(string accountId)
        {
            //try
            //{
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.CheckReceiverExists, accountId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader srd = cmd.ExecuteReader();
                    Console.WriteLine("Enter the line");
                    while (srd.Read())
                    {
                        var store = Convert.ToInt32(srd.GetValue(0));
                        Console.WriteLine(store);
                        if (store == 1)
                            return true;
                    }
                    return false;
                }
                }
            /*}
            catch
            {
                return false;
            }*/
        }



        public bool CheckBankExists(string bankId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(String.Format(SqlQueries.CheckBankExsits), bankId), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();
                        if (srd.Read())
                        {
                            int count = (int)srd.GetValue(0);
                            if (count == 1)
                                return true;
                        }
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
        public bool AuthenticateStaff(string staffId, string password)
        {
            try 
            {     
        
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.AuthenticateStaffExists, staffId, password), conn))
                    {
                        cmd.Connection.Open();
                        MySqlDataReader srd = cmd.ExecuteReader();
                        
                        while (srd.Read())
                        {
                            
                            
                            if (Convert.ToInt32(srd.GetValue(0)) == 1)
                                cmd.Connection.Close();
                            
                            return true;
                        }
                        cmd.Connection.Close();
                        
                        return false;
                    }
                    
                }
            }
            catch
            {
                return false;
            }
        }

        public List<(float amount, string sourceId, string recieverId, int type, DateTime dateTime)> GetTransaction(string accountId)
        {
            var transactioDetails = new List<(float amount, string sourceId, string recieverId, int type, DateTime dateTime)> { };
            Console.WriteLine("Here");
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand(String.Format(SqlQueries.GetTransactions, accountId), conn))
                {
                    cmd.Connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int type = (int)Enum.Parse(typeof(TransactionType), reader.GetValue(2).ToString());
                        Console.WriteLine(type);
                        string senderId = "";
                        string receiverId = "";
                        try
                        {
                        senderId = reader.GetString(3); }
                        catch
                        {
                            senderId = "";
                        }
                        try
                        {
                             receiverId =reader.GetString(4); }
                        catch
                        {
                            receiverId = "";
                        }
                        transactioDetails.Add((Convert.ToSingle(reader.GetValue(1)), senderId, receiverId, type, reader.GetDateTime(5)));
                    }
                }
            }
            return transactioDetails;
        }
    } 
}
