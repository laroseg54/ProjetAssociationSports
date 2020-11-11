using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetAssociationSports.Models
{
    public class Adherent
    {
        public Adherent(string numCompte, DateTime dateNaissance, string numTel)
        {

            DateNaissance = dateNaissance;
            ResteaPayer = 0;
            Creneaux = new SortedSet<Creneau>();
            ApplicationUserID = numCompte;
        }

        public int Id { get; set; }
        public string ApplicationUserID { get; set; }
        public double ResteaPayer { get; set; }
        public DateTime DateNaissance { get; set; }
        public string NumTel { get; set; }
        public virtual ApplicationUser Compte { get; set; }

        public virtual SortedSet<Creneau> Creneaux { get; set; }

    }
}
