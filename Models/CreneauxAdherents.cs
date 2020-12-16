using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models
{
    public class CreneauxAdherents
    {
        [Key, Column(Order = 0)]
        public string AdherentID { get; set; }
        [Key, Column(Order = 1)]
        public int CreneauID { get; set; }

        public virtual Adherent Adherent { get; set; }
        public virtual Creneau Creneau { get; set; }
        [Required]
        public bool estPaye { get; set; }
    }
}