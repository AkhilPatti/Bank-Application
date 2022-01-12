using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BankApp.Models
{
    public class Transaction
    {
        [Required]
        [Key]
        [Column("TransactionId")]
        public string tranactionId { get; set; }
        [Required]
        [Column("SenderId")]
        [ForeignKey("UserAccounts")]
        public string sourceAccountId { get; set; }
        [Required]
        [Column("ReceiverId")]
        [ForeignKey("UserAccounts")]
        public string receiveraccountId { get; set; }
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
