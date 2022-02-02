using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BankApp.Models
{
    [Table("Banks", Schema = "practice")]
    public class Bank
    {
        [Key]
        [Required]
        [MaxLength(12)]
        [Column("BankId")]
        public string bankId { get; set; }
        [Column("BankName")]
        [MaxLength(25)]
        [Required]
        public string bankName { get; set; }
        [Column("sImps")]
        [Required]
        public double sImps { get; set; }
        [Column("oImps")]
        [Required]
        public double oImps { get; set; }
        [Required]
        [Column("sRtgs")]
        public double sRtgs { get; set; }
        [Required]
        [Column("oRtgs")]
        public double oRtgs { get; set; }

        public ICollection<Account> accounts { get; set; }
        public ICollection<BankStaff> bankStaff{get;set;}

        public ICollection<BankCurrencies> bankCurrencies { get; set; }
    }
    }
