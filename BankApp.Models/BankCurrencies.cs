using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace BankApp.Models
{
    [Table("BankCurrencies",Schema ="practice")]
    public class BankCurrencies
    {
        [Required]
        [MaxLength(25)]
        [ForeignKey("Banks")]
        [Column("BankId")]
        public string bankId { get; set; }
        public Bank bank { get; set; }
        [Required]
        
        [ForeignKey("Currencies")]
        [MaxLength(25)]
        [Column("CurrencyCode")]
        public string currerncyCode { get; set; }
        public Currency currency { get; set; }
    }
}
