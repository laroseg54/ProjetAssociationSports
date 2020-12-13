using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models
{
    public class Discipline
    {


        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Section> Sections { get; set; }

        public Discipline(string nom, string description)
        {
            Nom = nom;
            Description = description;
            Sections = new List<Section>();
        }

        public Discipline() { }


    }
}
