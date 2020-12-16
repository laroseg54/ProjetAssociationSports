using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.ViewModels
{
    public class AdherentViewModel : IValidatableObject
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date de naissance requise ")]
        [Display(Name = "Date de Naissance")]
        public DateTime DateNaissance { get; set; }

        [Phone]
        [Display(Name = "Numéro de téléphone")]
        public string NumTel { get; set; }
        [Required(ErrorMessage = " Vous devez entrer votre nom ")]
        [StringLength(25)]
        public string Nom { get; set; }
        [StringLength(25)]
        [Required(ErrorMessage = "Vous devez entrer votre prénom ")]
        public string Prenom { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.DateNaissance.Year < 1920)
                yield return new ValidationResult("Date de naissance non valide (trop vieux) ", new[] { "DateNaissance" });
            if (this.DateNaissance.Year > 2016)
                yield return new ValidationResult("Date de naissance non valide(Trop jeune)", new[] { "DateNaissance" });
       
        }
    }
}