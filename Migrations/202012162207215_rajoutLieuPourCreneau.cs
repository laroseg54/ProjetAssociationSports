namespace ProjetAssociationSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rajoutLieuPourCreneau : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creneaux", "Lieu", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creneaux", "Lieu");
        }
    }
}
