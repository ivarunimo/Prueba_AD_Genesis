using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextoDB.Tables
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string username { get; set; }

        [Required]
        [MaxLength(500)]
        public required string password { get; set; }
        [Required]
        [MaxLength(250)]
        public required string fullName { get; set; }

        [Required]
        [MaxLength(150)]
        public required string email { get; set; }
    }
}
