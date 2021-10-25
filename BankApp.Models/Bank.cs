using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Models
{
    public class Bank
    {
        public string  Id { get; set; }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
        public DateTime CreateOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn {get;set;}
        public string UpdatedBy { get; set; }
        }
    }
