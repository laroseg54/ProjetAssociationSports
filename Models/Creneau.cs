using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models
{
    public class Creneau
    {
        public Creneau(string encadrantId ,int sectionID, DayOfWeek jour, string heureDeb, string heureFin, int nbrPlacesLimite, string lieu)
        {
            SectionID = sectionID;
            Jour = jour;
            HeureDeb = heureDeb;
            HeureFin = heureFin;
            NbrPlacesLimite = nbrPlacesLimite;
            NbrPlaceRestantes = nbrPlacesLimite;
            CreneauxAdherents = new List<CreneauxAdherents>();
            ApplicationUserID = encadrantId;
            Lieu = lieu;


        }

        public Creneau() { }


        public int Id { get; set; }
        [Required]
        public int SectionID { get; set; }
        [Required]
        public string ApplicationUserID { get; set; }
        [Required]
        public DayOfWeek Jour { get; set; }
        [Required]
        public string HeureDeb { get; set; }
        [Required]
        public string HeureFin { get; set; }
        [Required]
        public string Lieu { get; set; }
        [Required]
        public int NbrPlacesLimite { get; set; }
        public int NbrPlaceRestantes { get; set; }
        public virtual Section Section { get; set; }
        public virtual ApplicationUser Encadrant {get;set;}
        public virtual ICollection<CreneauxAdherents> CreneauxAdherents { get; set; }


    }
}
