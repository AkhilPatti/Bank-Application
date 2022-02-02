using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    [Table("StaffAccounts",Schema ="practice")]
    public class BankStaff
    {
        [Required]
        [MaxLength(25)]
        [Column("Name")]
        public string staffName { get; set; }
        [Required]
        [Column("StaffId")]
        [Key]
        [MaxLength(25)]
        public string staffId { get; set; }
        [Required]
        [Column("Password")]
        public string password { get; set; }
        /*[Required]
        public Genders gender { get; set; }*/
        [Required]
        [Column("BankId")]
        [ForeignKey("Bank")]
        [MaxLength(25)]
        public string bankId { get; set; }
        public Bank bank { get; set; }
    }
}
