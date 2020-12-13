using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models.CustomValidation
{
    public class TypePieceValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string val = (string)value;
            if (val == TypePiece.Assurance || val == TypePiece.FicheRenseignement || val == TypePiece.CertifMedicale)
            {
                return true;
            }
            return false;
        }
    }
}