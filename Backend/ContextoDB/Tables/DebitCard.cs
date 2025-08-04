using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextoDB.Tables
{
    public class DebitCard
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(16)]
        public required string number { get; set; }

        [Required]
        public bool active { get; set; } = true;

        [Required]
        public int id_account { get; set; }
        [Required]
        public required string type { get; set; }

        [Required]
        public required string expDate { get; set; }
        [Required]
        public required string name_card { get; set; }

        [ForeignKey("id_account")]
        public virtual Account? Account { get; set; }

    }
}
