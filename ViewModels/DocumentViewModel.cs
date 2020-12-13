using ProjetAssociationSports.Models;
using ProjetAssociationSports.Models.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ProjetAssociationSports.ViewModels
{
    public class DocumentViewModel
    {
        [Required]
        [DocumentValidateAttributes(ErrorMessage = "Ce type de fichier n'est pas autorisé")]
        public HttpPostedFileBase PostedFile { get; set; }
        
        [Required]
        [TypePieceValidation]
        public string TypePiece { get; set; }

    }
}