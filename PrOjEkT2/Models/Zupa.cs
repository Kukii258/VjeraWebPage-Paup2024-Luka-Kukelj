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
        [Required(ErrorMessage = "{0} je obavezno")]
        public int Id { get; set; }

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        public string Ime { get; set; }
        [Column("broj_crkvi")]
        [Required(ErrorMessage = "{0} je obavezno")]
        [Display(Name = "BrojCrkvi")]
        public int BrojCrkvi { get; set; }
        [Required(ErrorMessage = "{0} je obavezno")]
        [Column("broj_vjernika")]
        [Display(Name = "BrojVjernika")]
        public int BrojVjernika { get; set; }
        [Required(ErrorMessage = "{0} je obavezno")]
        [Column("broj_svecenika")]
        [Display(Name = "BrojSvecenika")]
        public int BrojSvecenika { get; set; }
    }
}