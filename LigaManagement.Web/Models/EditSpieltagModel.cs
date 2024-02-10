using LigaManagement.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace LigaManagement.Web.Models
{
    public class EditSpieltagModel
    {
        public int SpieltagId { get; set; }
        
        [Required(ErrorMessage = "Verein 1 muß angegeben werden")]
        [MinLength(2)]
        [ValidateComplexType]
        public Verein Verein1 { get; set; } = new Verein();
      
        [Required(ErrorMessage = "Verein 2 muß angegeben werden")]
        [MinLength(2)]
        [ValidateComplexType]
        public Verein Verein2 { get; set; } = new Verein();

        [Required(ErrorMessage = "Tore 1 muß angegeben werden")]
        [MinLength(2)]
        [ValidateComplexType]
        public int Tore1 { get; set; }

        [Required(ErrorMessage = "Tore 2 muß angegeben werden")]
        [MinLength(2)]
        [ValidateComplexType]
        public int Tore2 { get; set; }

        [Required(ErrorMessage = "Ort muß angegeben werden")]
        [MinLength(10)]
        public string Ort { get; set; }

        public DateTime Datum { get; set; }
    }
}
