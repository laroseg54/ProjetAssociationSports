using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models
{
    public class Adherent
    {

      

        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        [Display(Name = "Reste à payer")]
        public double ResteaPayer { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date de naissance requise ")]
        [Display(Name = "Date de naissance")]
        public DateTime DateNaissance { get; set; }

        [Phone]
        [Display(Name = "Numéro de téléphone")]
        public string NumTel { get; set; }
        [Required(ErrorMessage = " Vous devez entrer votre nom ")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Vous devez entrer votre prénom ")]
        public string Prenom { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<CreneauxAdherents> CreneauxAdherents { get; set; }

        public virtual ICollection<Document> Documents { get; set; }


    }
}
