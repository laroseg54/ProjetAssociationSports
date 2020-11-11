using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models
{
    public class Discipline : IComparable<Discipline>
    {


        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        public virtual SortedSet<Section> Sections { get; set; }

        public Discipline(string nom, string description)
        {
            Nom = nom;
            Description = description;
            Sections = new SortedSet<Section>();
        }

        public Discipline() { }

        public int CompareTo(Discipline obj)
        {
            return Nom.CompareTo(obj.Nom);
        }
    }
}
