namespace ProjetAssociationSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class encandrement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creneaux", "ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Creneaux", "HeureDeb", c => c.String(nullable: false));
            AlterColumn("dbo.Creneaux", "HeureFin", c => c.String(nullable: false));
            CreateIndex("dbo.Creneaux", "ApplicationUserID");
            AddForeignKey("dbo.Creneaux", "ApplicationUserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Creneaux", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Creneaux", new[] { "ApplicationUserID" });
            AlterColumn("dbo.Creneaux", "HeureFin", c => c.String());
            AlterColumn("dbo.Creneaux", "HeureDeb", c => c.String());
            DropColumn("dbo.Creneaux", "ApplicationUserID");
        }
    }
}
