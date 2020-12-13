using ProjetAssociationSports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.ViewModels
{
    public class AdherentDetailsViewModel
    {
        public AdherentDetailsViewModel() { }

        public DocumentViewModel DocumentViewModel { get; set; }
        public Adherent Adherent { get; set; }
    }
}