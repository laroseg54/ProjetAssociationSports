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



            context.Creneaux.RemoveRange(context.Creneaux);
            context.Sections.RemoveRange(context.Sections);
            context.Disciplines.RemoveRange(context.Disciplines);

            Discipline D1 = new Discipline("judo", "sport de combat japonais, la castagne tout ça tout ça ");
            Discipline D2 = new Discipline("natation", "nager dans l'eau ça fait du bien ");

            context.Disciplines.Add(D1);
            context.Disciplines.Add(D2);
            context.SaveChanges();
            Section S1 = new Section(D1.Id, "judo compétition", "le judo ou ça castagne dure pour gagner des sous blyat", 42);
            context.Sections.Add(S1);
            Section S2 = new Section(D1.Id, "judo loisir", "le judo ou ça glande", 37);
            context.Sections.Add(S2);
            Section S3 = new Section(D2.Id, "natation compét", "la natation ou ça nage dure pour gagner des sous blyat", 88);
            context.Sections.Add(S3);
            Section S4 = new Section(D2.Id, "natation loisir", "la natation ou ça glande", 99);
            context.Sections.Add(S4);
            context.SaveChanges();
            Creneau C1 = new Creneau(S1.Id, DayOfWeek.Friday, "14h00", "15h30", 20);
            context.Creneaux.Add(C1);
            Creneau C2 = new Creneau(S2.Id, DayOfWeek.Monday, "9h00", "12h00", 60);
            context.Creneaux.Add(C2);
            context.SaveChanges();
        }
    }
}