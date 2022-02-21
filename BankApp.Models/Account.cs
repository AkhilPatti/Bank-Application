using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BankApp.Models
{
    [Table("UserAccounts", Schema = "practice")]
    public class Account
    {
        [Required]
        [Key]
        [Column("AccountId")]
        public string accountId { get; set; }
        [Required]
        [Column("Name")]
        public string accountHolderName { get; set; }
        [Required]
        [Column("Pin")]
        [MaxLength(200)]
        public string pinHash { get; set; }
        [Required]
        [Column("PhoneNumber")]
        public string phoneNumber { get; set; }
        [Required]
        [Column("Balance")]
        public float balance { get; set; }
        [Required]
        //[ForeignKey("Banks")]
        [Column("BankId")]
        public string bankId { get; set; }
        public Bank bank { get; set; }
        
        public ICollection<Transaction> rtransactions {get; set;}
        public ICollection<Transaction> stransactions { get; set; }
        
        
    }
}
