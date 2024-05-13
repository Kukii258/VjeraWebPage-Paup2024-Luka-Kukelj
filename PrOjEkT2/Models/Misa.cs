using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrOjEkT2.Models
{

    [Table("misa")]
    public class Misa
    {
        [Key]
        [Display(Name = "IdMisa")]
        public int Id { get; set; }

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} mora biti duljine minimalno {2} a maskimalno {1} znakova")]
        public string Ime { get; set; }

        [Display(Name = "DatumVrijemeMise")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} je obavezno")]
        [DataType(DataType.Date)]
        public DateTime DatumVrijemeMise { get; set; }

        [Display(Name = "IdCrkva")]
        public int IdCrkva { get; set; }

        [Display(Name = "IdZupa")]
        public int IdZupa { get; set; }

        [Display(Name = "IdSvecenik")]
        public int IdSvecenik { get; set; }

        [Display(Name = "Crkva")]
        public string Crkva { get; set; }

        public string Svecenik { get; set; }

    }
}