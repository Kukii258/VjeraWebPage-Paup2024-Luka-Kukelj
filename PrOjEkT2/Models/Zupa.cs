using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrOjEkT2.Models
{
    [Table("zupa")]
    public class Zupa
    {
        [Display(Name = "IdZupa")]
        public int Id { get; set; }

        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Column("broj_crkvi")]
        [Display(Name = "BrojCrkvi")]
        public int BrojCrkvi { get; set; }
        [Column("broj_vjernika")]
        [Display(Name = "BrojVjernika")]
        public int BrojVjernika { get; set; }
        [Column("broj_svecenika")]
        [Display(Name = "BrojSvecenika")]
        public int BrojSvecenika { get; set; }
    }
}