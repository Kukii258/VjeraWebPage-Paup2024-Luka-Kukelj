using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrOjEkT2.Models
{
    [Table("ovlasti")]
    public class Ovlast
    {
        [Key]
        public string sifra { get; set; }
        public string naziv { get; set; }
    }
}