using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextoDB.Tables
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string number { get; set; }

        [Required]
        public double balance { get; set; }

        [Required]
        [MaxLength(50)]
        public required string account_name { get; set; }

        [Required]
        public int id_user { get; set; }

        [ForeignKey("id_user")]
        public virtual User? User { get; set; }



    }
}
