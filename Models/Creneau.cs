using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models
{
    public class Creneau
    {
        public Creneau(int sectionID, DayOfWeek jour, string heureDeb, string heureFin, int nbrPlacesLimite)
        {
            SectionID = sectionID;
            Jour = jour;
            HeureDeb = heureDeb;
            HeureFin = heureFin;
            NbrPlacesLimite = nbrPlacesLimite;
            NbrPlaceRestantes = nbrPlacesLimite;
            Adherents = new List<Adherent>();
            
        }

        public Creneau() { }


        public int Id { get; set; }
        public int SectionID { get; set; }

        public DayOfWeek Jour { get; set; }
        public string HeureDeb { get; set; }
        public string HeureFin { get; set; }
        public int NbrPlacesLimite { get; set; }
        public int NbrPlaceRestantes { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<Adherent> Adherents { get; set; }


    }
}
