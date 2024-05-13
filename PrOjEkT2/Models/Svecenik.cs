using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrOjEkT2.Models
{
    [Table("svecenik")]
    public class Svecenik
    {
        [Display(Name = "ID Svecenika")]
        public int Id { get; set; }

        [Display(Name = "Ime")]
        public string Ime { get; set; }

        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Display(Name = "IdCrkva")]
        public int IdCrkve { get; set; }

        [Display(Name = "IdZupa")]
        public int IdZupe { get; set; }

        [Display(Name ="Crkva")]
        public string crkva { get; set; }
        public string zupa { get; set; }

    }
}