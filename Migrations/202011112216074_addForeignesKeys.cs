namespace ProjetAssociationSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addForeignesKeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adherents", "ApplicationUserID", c => c.String(maxLength: 128));
            AddColumn("dbo.Creneaux", "ApplicationUserID", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Nom", c => c.String());
            AddColumn("dbo.AspNetUsers", "Prenom", c => c.String());
            CreateIndex("dbo.Adherents", "ApplicationUserID");
            CreateIndex("dbo.Creneaux", "ApplicationUserID");
            AddForeignKey("dbo.Adherents", "ApplicationUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Creneaux", "ApplicationUserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Creneaux", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Adherents", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Creneaux", new[] { "ApplicationUserID" });
            DropIndex("dbo.Adherents", new[] { "ApplicationUserID" });
            DropColumn("dbo.AspNetUsers", "Prenom");
            DropColumn("dbo.AspNetUsers", "Nom");
            DropColumn("dbo.Creneaux", "ApplicationUserID");
            DropColumn("dbo.Adherents", "ApplicationUserID");
        }
    }
}
