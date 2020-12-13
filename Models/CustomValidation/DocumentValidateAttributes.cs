using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models.CustomValidation
{
    public class DocumentValidateAttributes : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            string[] _validExtensions = { "PDF", "JPEG", "PNG" };

            var file = (HttpPostedFileBase)value;
            var ext = Path.GetExtension(file.FileName).ToUpper().Replace(".", "");
            return _validExtensions.Contains(ext); 
        }
    }
}