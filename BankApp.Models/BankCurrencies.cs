using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace BankApp.Models
{
    [Table("BankCurrencies",Schema ="bankdatabase")]
    public class BankCurrencies
    {
        [Required]
        
        [ForeignKey("Banks")]
        [Column("BankId")]
        public string bankId { get; set; }
        [Required]
        
        [ForeignKey("Currencies")]
        [Column("CurrencyCode")]
        public string currerncyCode { get; set; }
    }
}
