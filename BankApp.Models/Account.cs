using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BankApp.Models
{
    [Table("UserAccounts", Schema = "bankdatabase")]
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
        public string pin { get; set; }
        [Required]
        [Column("PhoneNumber")]
        public string phoneNumber { get; set; }
        [Required]
        [Column("Balance")]
        public float balance { get; set; }
        [Required]
        [ForeignKey("Banks")]
        [Column("BankId")]
        public string bankId { get; set; }
        
       

    }
}
