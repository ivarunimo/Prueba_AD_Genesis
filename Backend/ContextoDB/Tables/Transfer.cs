using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextoDB.Tables
{
    public class Transfer
    {
        public int Id { get; set; }
        public int origin { get; set; }
        public int destiny { get; set; }
        public required string description { get; set; }
        public double amount { get; set; }

        [ForeignKey("origin")]
        public virtual Account? OriginAccount { get; set; }
        [ForeignKey("destiny")]
        public virtual Account? DestinyAccount { get; set; }
    }
}
