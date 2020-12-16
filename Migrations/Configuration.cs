namespace ProjetAssociationSports.Migrations
{
    using Microsoft.Ajax.Utilities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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


            context.CreneauxAdherents.RemoveRange(context.CreneauxAdherents);
            context.Creneaux.RemoveRange(context.Creneaux);
            context.Sections.RemoveRange(context.Sections);
            context.Disciplines.RemoveRange(context.Disciplines);
            context.Adherents.RemoveRange(context.Adherents);
           

            Discipline D1 = new Discipline("judo", "Le judo est un art martial et un sport de combat d'origine japonaise");
            Discipline D2 = new Discipline("natation sportive", "natation sportive consiste à parcourir dans une piscine, le plus rapidement possible et dans un style codifié par la Fédération internationale de natation (FINA), une distance donnée, sans l'aide d'aucun accessoire.");
            Discipline D3 = new Discipline("escrime", "L'escrime est un sport de combat. Il s'agit de l'art de toucher un adversaire avec la pointe ou le tranchant (estoc et taille) d'une arme blanche");
            context.Disciplines.Add(D1);
            context.Disciplines.Add(D2);
            context.SaveChanges();
            Section S1 = new Section(D1.Id, "judo compétition", "pratique du judo pour personnes experimentées, compétitions obligatoires", 42);
            context.Sections.Add(S1);
            Section S2 = new Section(D1.Id, "judo loisir", "ouvert à tous à partir de 18 ans", 74);
            context.Sections.Add(S2);
            Section S3 = new Section(D2.Id, "natation homme amateur", "ouvert aux personnes souhaitant débuter dans l'exercice de la natation", 88);
            context.Sections.Add(S3);
            Section S4 = new Section(D2.Id, "natation loisir", "accés à la piscine annuel pour pratique libre", 99);
            context.Sections.Add(S4);
            context.SaveChanges();
           
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string roleName = "Encadrant";
            var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            role.Name = roleName;
            if (!RoleManager.RoleExists(roleName))
            {

                RoleManager.Create(role);

            }

            context.SaveChanges();
            var user = new ApplicationUser
            {
                UserName = "Encadrant@email.com",
                Email = "Encadrant@email.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),


            };

            var store = new UserStore<ApplicationUser>(context);
            var manager = new ApplicationUserManager(store);

            manager.Create(user, "DOTNET2!");
            context.SaveChanges();
            user = context.Users.FirstOrDefault(u => u.Email == "Encadrant@email.com");
            manager.AddToRole(user.Id, "Encadrant");
            context.SaveChanges();
            Creneau C1 = new Creneau(user.Id, S1.Id, DayOfWeek.Friday, "14h00", "15h30", 20,"Palais de l'élysée");
            context.Creneaux.Add(C1);
            Creneau C2 = new Creneau(user.Id, S2.Id,DayOfWeek.Saturday,"9h00", "12h00", 60,"Gymnase Stanislas");
            context.Creneaux.Add(C2);
            Creneau C3 = new Creneau(user.Id, S1.Id, DayOfWeek.Monday, "9h30", "11h00", 0,"Chez michou");
            context.Creneaux.Add(C2);
            context.SaveChanges();

        }
    }
}