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
            : base("name=AssociationContext", throwIfV1Schema: false)
        {

        }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        //public DbSet<Compte> Comptes { get; set; }

        public virtual DbSet<Creneau> Creneaux { get; set; }

        public virtual DbSet<Adherent> Adherents { get; set; }

        public virtual DbSet<Document> Documents { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}