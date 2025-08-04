using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextoDB.Tables
{
    public class Movement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int origin { get; set; }
        [Required]
        [MaxLength(250)]
        public required string description { get; set; }

        [Required]
        public double amount { get; set; }

        [Required]
        public int type { get; set; } // 1 = Ingreso, 2 = Egreso

        [Required]
        public int kind { get; set; } // 1 = Transferencia, 2 = Retiro, 3 = Deposito, 4 = Pago de servicios, 5 = Gasto General

        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public int id_user { get; set; }

        [ForeignKey("origin")]
        public virtual Account? Account { get; set; }

        [ForeignKey("id_user")]
        public virtual User? User { get; set; }

    }
}
