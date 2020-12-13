using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models
{
 
    public static class TypePiece{

        public const string FicheRenseignement = "FicheRenseignement" ;
        public const string Assurance = "Assurance";
        public const string CertifMedicale = "CertifMedicale";

    }
    public class Document
    {

        public int Id { get; set; }

        public string AdherentID { get; set; }
        public string Nom { get; set; }
        
        public string TypeDoc { get; set; }
        
        public string TypePiece { get; set; }
        
        public string Chemin { get; set; }

        [Required]
        public virtual Adherent Adherent { get; set; }

    }
}