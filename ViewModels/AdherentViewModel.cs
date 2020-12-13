using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.ViewModels
{
    public class AdherentViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date de naissance requise ")]
        public DateTime DateNaissance { get; set; }

        [Phone]
        [Display(Name = "Numéro de téléphone")]
        public string NumTel { get; set; }
        [Required(ErrorMessage = " Vous devez entrer votre nom ")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Vous devez entrer votre prénom ")]
        public string Prenom { get; set; }
    }
}