
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    [Table("currencies",Schema ="bankdatabase")]
    public class Currency
    {
        [Required]
        [Key]
        [Column("CurrencyCode")]
        public string currencyCode { get; set; }
        [Required]
        [Column("ExchangeRate")]
        public double exchangeRate { get; set; }
        [Required]
        [Column("Name")]
        public string name { get; set; }

    }
}
