using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    [Table("StaffAccounts",Schema ="bankdatabase")]
    public class BankStaff
    {
        [Required]
        [Column("Name")]
        public string staffName { get; set; }
        [Required]
        [Column("StaffId")]
        [Key]
        public string staffId { get; set; }
        [Required]
        [Column("Password")]
        public string password { get; set; }
        /*[Required]
        public Genders gender { get; set; }*/
        [Required]
        [Column("BankId")]
        [ForeignKey("Bank")]
        public string bankId { get; set; }
    }
}
