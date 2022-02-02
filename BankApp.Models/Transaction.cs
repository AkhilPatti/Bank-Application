using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BankApp.Models
{
    [Table("Transactions", Schema = "practice")]
    public class Transaction
    {
        [Required]
        [Key]
        [MaxLength(90)]
        [Column("TransactionId")]
        public string transactionId { get; set; }
        [Required]
        [Column("SenderId")]
        [MaxLength(25)]
        [ForeignKey("UserAccounts")]
        public string sourceAccountId { get; set; }
        public Account sourceAccount {get;set;}
        [Required]
        [Column("ReceiverId")]
        [MaxLength(25)]
        [ForeignKey("UserAccounts")]
        public string receiveraccountId { get; set; }
        public Account receiverAccount { get; set; }
        [Required]
        [Column("Amount")]
        public float amount { get; set; }
        [Required]
        [Column("TransactionType")]
        public TransactionType type {get; set;}
        [Required]
        [Column("Time")]
        public DateTime on { get; set; }
        
        
        
    }
}
