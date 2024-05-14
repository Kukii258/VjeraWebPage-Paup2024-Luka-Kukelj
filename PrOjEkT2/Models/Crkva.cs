using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrOjEkT2.Models
{



    [Table("crkva")]

    public class Crkva
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} je obavezno")]
        [Display(Name = "Ime")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "{0} je obavezno")]
        [Display(Name = "Adresa")]
        public string Adresa { get; set; }

        [Display(Name = "BrojVjernika")]
        public int BrojVjernika { get; set; }

        [Display(Name = "IdSvecenika")]
        public int IdSvecenika { get; set; }

        [Display(Name = "IdZupe")]
        public int IdZupa { get; set; }

        [Display(Name = "Zupa")]
        public string zupa { get; set; }



    }


}