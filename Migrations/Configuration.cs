namespace ProjetAssociationSports.Migrations
{
    using Microsoft.Ajax.Utilities;
    using ProjetAssociationSports.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjetAssociationSports.dal.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjetAssociationSports.dal.ApplicationDbContext context)
        {

  


            var disciplines = new List<Discipline>
            {

                new Discipline("judo","sport de combat japonais, la castagne tout �a tout �a "),
                new Discipline("natation","nager dans l'eau �a fait du bien "),


            };

            disciplines.ForEach(s => context.Disciplines.Add(s));

            var sections = new List<Section>
            {
                new Section(1,"judo comp�tition","le judo ou �a castagne dure pour gagner des sous blyat",42),
                new Section(1,"judo loisir","le judo ou �a glande",37),
                new Section(2,"natation comp�t","la natation ou �a nage dure pour gagner des sous blyat",88),
                new Section(2,"natation loisir","la natation ou �a glande",99),
            };


            sections.ForEach(s => context.Sections.Add(s));

            var creneaux = new List<Creneau>
            {
                new Creneau(1,DayOfWeek.Friday,"14h00","15h30",20)


            };
            creneaux.ForEach(s => context.Creneaux.Add(s));
            context.SaveChanges();
        }
    }
}

