using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services
{
    class SqlQueries
    {

        public static string GetBalance = "Select Balance from bankdatabase.UserAccounts where AccountId='{0}';";
        public static string GetName = "Selct Name from bankdatabase.UserAccounts where AccountId='{0}';";
        public static string GetUserBankId = "Select BankId from bankdatabase.Useraccounts where AccountId='{0}';";
        public static string GetStaffBankId = "Select BankId from bankdatabase.StaffAccounts where StaffId='{0}';";
        public static string GetStaffPassword = "Select `Password` from bankdatabase.StaffAccounts where StaffId='{0}';";
        public static string GetsRTGSCharges = "Select srtgs from bankdatabase.banks where BankId='{0}'; ";
        public static string GetoRTGSCharges = "Select ortgs from bankdatabase.banks where BankId='{0}'; ";
        public static string GetsIMPSCharges = "Select sImps from bankdatabase.banks where BankId='{0}'; ";
        public static string GetoIMPSCharges = "Select sImps from bankdatabase.banks where BankId='{0}'; ";
        public static string GetTransactions = "SELECT * from `bankdatabase`.`transactions` WHERE ReceiverId = '{0}' or SenderId ='{0}';";
        public static string GetTransactionAmount = "Select Amount from bankdatabase.Trasnactions where TransactionId='{0}'";
        public static string GetTransactionType = "Select TransactionType from bankdatabase.Transaction where TransactionId='{0}'";
        public static string GetReceiverId = "Select ReceiverId from bankdatabase.Transactions where bankdatabase where TransactionId='{0}'";
        public static string GetSenderId = "Select SenderId from bankdatabase.Transactions where TrasnactionId='{0}'";
        public static string GetExchangeRate = "Select ExchangeRate from bankdatabase.Currencies where CurrencyCode = '{0}';";
        public static string GetTrasnaction = "Selct * from bankdatabase.Transactions WHERE TransactionId='{0};";
        public static string GetCurrencies = "SELECT CurrencyCode FROM bankdatabase.BankCurrencies WHERE `bankId` = '{0}';";

        public static string CheckAcceptedAurrency = "Select * from bankdatabase.bankcurrencies WHERE `BankId` = '{0}' AND `CurrencyCode`='{1}';";

        public static string AddUser = "INSERT INTO `bankdatabase`.`UserAccounts` (AccountId,`Name`,`Pin`,`PhoneNumber`,`BankId`) VALUES ('{0}','{1}','{2}','{3}','{4}');";
        public static string AddStaff = "INSERT INTO bankdatabase.StaffAccounts (StaffId,Name,Password,BankId) VALUES ('{0}','{1}','{2}','{3}');";
        public static string AddCurrency = "INSERT INTO bankdatabase.Currencies (CurrencyCode,Name,ExchangeRate) VALUES ('{0}','{1}','{2}');";
        public static string AddAcceptedCurrency = "INSERT INTO `BankCurrencies` (BankId,CurrencyCode) VALUES ('{0}','{1}');";
        public static string AddBank = "INSERT INTO bankdatabase.banks (BankId,BankName) VALUES ('{0}','{1}');";
        public static string AddTransaction = "INSERT INTO bankdatabase.Transactions(TransactionId,Amount,ReceiverId,SenderId,TransactionType,`Time`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}');";
        public static string AddDepositTransaction = "INSERT INTO bankdatabase.Transactions(TransactionId,Amount,ReceiverId,TransactionType,`Time`) VALUES ('{0}','{1}','{2}','{3}','{4}');";
        public static string AddWithdrawlTransaction = "INSERT INTO bankdatabase.Transactions(TransactionId,Amount,SenderId,TransactionType,`Time`) VALUES ('{0}','{1}','{2}','{3}','{4}');";
        public static string DeleteUser = "DELTE FROM bankdatabase.Accounts WHERE AccountId='{0}' ";

        public static string UpdateBalance = "UPDATE `bankdatabase`.`UserAccounts` SET `Balance` ='{0}' WHERE `AccountId`='{1}';";
        public static string UpdatPassword = "UPDATE `bankdatabase`.`UserAccounts` SET `Password` ='{0}' WHERE `AccountId`='{1}';";
        public static string UpdatesRTGS = "UPDATE `bankdatabase`.`banks` SET `sRTGS` = '{0}' WHERE `BankId`='{1}';";
        public static string UpdateoRTGS = "UPDATE `bankdatabase`.`banks` SET `oRTGS` = '{0}' WHERE `BankId`='{1}';";
        public static string UpdatesIMPS = "UPDATE `bankdatabase`.`banks` SET `sIMPS` = '{0}' WHERE `BankId`='{1}';";
        public static string UpdateoIMPS = "UPDATE `bankdatabase`.`banks` SET `oIMPS` = '{0}' WHERE `BankId`='{1}';";


        public static string CheckTabelsExist = "SELECT count(*) FROM information_schema.tables WHERE table_schema = 'bankdatabase' AND table_name = 'banks' LIMIT 1;";
        public static string CheckCurrencyExist = "SELECT count(CurrencyCode) FROM Currencies WHERE `CurrencyCode`='{0}';";
        public static string CheckBankExsits = "SELECT count(`BankId`) FROM `Bankdatabase`.`banks` WHERE `BankId`='{0}';";
        public static string CheckReceiverExists = "SELECT count(AccountId) FROM `Bankdatabase`.`UserAccounts` WHERE `AccountId`= '{0}';";
        public static string AuthenticateStaffExists = "SELECT count(*) FROM `Bankdatabase`.`StaffAccounts` WHERE `StaffId`= '{0}' AND `Password`= '{1}';";
        public static string AuthenticateCustomerExists = "SELECT count(AccountId) FROM `Bankdatabase`.`UserAccounts` WHERE `AccountId`='{0}' AND `Pin`='{1}';";
       
        public static string CreateBanks = "CREATE TABLE `Banks`(`BankId` VARCHAR(12) NOT NULL PRIMARY KEY, `BankName` VARCHAR(25) NOT NULL, `sImps` DOUBLE NOT NULL default 0, `oImps` DOUBLE NOT NULL default 0, `sRtgs` DOUBLE NOT NULL default 0, `oRtgs` DOUBLE NOT NULL default 0 );";
        public static string CreateUserAccounts = "CREATE UserAccounts`( `AccountId` VARCHAR(12) NOT NULL PRIMARY  KEY, `Balance` float NOT NULL default 0, `Name` VARCHAR(25) NOT NULL, `Pin` VARCHAR(10) NOT NULL, `PhoneNumber` VARCHAR(12) NOT NULL, `TransactionId` VARCHAR(12) NOT NULL, `BankId` VARCHAR(12) NOT NULL, FOREIGN KEY(`BankId`) REFERENCES `Banks`(`BankId`) ON DELETE CASCADE );";

        public static string CreateTransactions = "CREATE `TransactionId` VARCHAR(12) NOT NULL PRIMARY KEY, `Amount` float NOT NULL, `ReceiverId` VARCHAR(12) , `SenderId` VARCHAR(12) , `TransactionType` ENUM('WithDrawl','Deposit','Transfer') NOT NULL, FOREIGN KEY(`SenderId`) REFERENCES `UserAccounts`(`AccountId`) ON DELETE SET NULL, FOREIGN KEY(`ReceiverId`) REFERENCES `UserAccounts`(`AccountId`) ON DELETE SET NULL, `Time` Time NOT NULL );";
        public static string CreateCurrencies = "CREATE TABLE `Currencies`( `CurrencyCode` VARCHAR(4) NOT NULL PRIMARY KEY, `Name` CHAR(25) NOT NULL, `ExchangeRate` float NOT NULL default 1, `BankId` VARCHAR(12) NOT NULL, FOREIGN KEY(`BankId`) REFERENCES `Banks`(`BankId`) );";

        public static string CreateStaffAccounts = "CREATE TABLE `StaffAccounts`( `StaffId` VARCHAR(12) NOT NULL PRIMARY KEY, `Name` VARCHAR(25) NOT NULL, `Password` VARCHAR(12) NOT NULL, `BankId` VARCHAR(12) NOT NULL, FOREIGN KEY(`BankId`) REFERENCES `Banks`(`BankId`) ON DELETE Cascade );";
        public static string CreateBankCurrecies = "Create Table `BankCurrencies`( `BankId` VARCHAR (12) NOT NULL, `CurrencyCode` VARCHAR(4) NOT NULL, FOREIGN KEY(`BankId`) REFERENCES `Banks`(`BankId`) ON DELETE CASCADE, FOREIGN KEY(`CurrencyCode`) REFERENCES `Currencies`(`CurrencyCode`) ON DELETE CASCADE);";


    }
}
