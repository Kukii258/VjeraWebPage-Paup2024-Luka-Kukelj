using PrOjEkT2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrOjEkT2.Models
{
    [Table("vjernik")]
    public class Vjernik
    {
        [Key]
        [Display(Name = "korisnicko_ime")]
        [Required]
        public string korisnicko_ime { get; set; }


        [Display(Name = "ime")]
        [Required]
        public string ime { get; set; }

        [Display(Name = "prezime")]
        [Required]
        public string prezime { get; set; }

        public string PrezimeIme
        {
            get { return prezime + " " + ime; }
        }

        [Display(Name = "email")]
        [Required]
        [EmailAddress]
        public string email { get; set; }

        public string Lozinka { get; set; }

        [Column("ovlast")]
        [Display(Name = "Ovlast")]
        [Required]
        [ForeignKey("Ovlast")]
        public string SifraOvlasti { get; set; }

        [Display(Name = "Ovlast")]
        public virtual Ovlast Ovlast { get; set; }


        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        [Required]
        [NotMapped]
        public string LozinkaUnos { get; set; }

        [Display(Name = "Lozinka ponovljena")]
        [DataType(DataType.Password)]
        [Required]
        [NotMapped]
        [Compare("LozinkaUnos", ErrorMessage = "Lozinke moraju biti jednake!")]
        public string LozinkaUnos2 { get; set; }


        public string zupa { get; set; }
        public string crkva { get; set; }


    }
}