using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models
{
    public class Section : IComparable<Section>
    {

        public Section(int idDiscipline, string nom, string description, double prix)
        {
            DisciplineID = idDiscipline;
            Nom = nom;
            Description = description;
            Prix = prix;
        }
        public Section() { }

        public int Id { get; set; }
        public int DisciplineID { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        public double Prix { get; set; }
        public virtual Discipline Discipline { get; set; }
        public int CompareTo(Section obj)
        {
            return Nom.CompareTo(obj.Nom);
        }
    }

}