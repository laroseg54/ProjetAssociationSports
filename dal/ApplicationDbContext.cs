using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjetAssociationSports.Models;

namespace ProjetAssociationSports.dal
{
   
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Section> Sections { get; set; }
        //public DbSet<Compte> Comptes { get; set; }

        public DbSet<Creneau> Creneaux { get; set; }

        public DbSet<Adherent> Adherents { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}