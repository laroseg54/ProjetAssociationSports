namespace ProjetAssociationSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rectificationCreneaux : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CreneauAdherents", "Creneau_Id", "dbo.Creneaux");
            DropForeignKey("dbo.CreneauAdherents", "Adherent_Id", "dbo.Adherents");
            DropIndex("dbo.CreneauAdherents", new[] { "Creneau_Id" });
            DropIndex("dbo.CreneauAdherents", new[] { "Adherent_Id" });
            CreateTable(
                "dbo.CreneauxAdherents",
                c => new
                    {
                        AdherentID = c.String(nullable: false, maxLength: 128),
                        CreneauID = c.Int(nullable: false),
                        estPaye = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.AdherentID, t.CreneauID })
                .ForeignKey("dbo.Adherents", t => t.AdherentID, cascadeDelete: true)
                .ForeignKey("dbo.Creneaux", t => t.CreneauID, cascadeDelete: true)
                .Index(t => t.AdherentID)
                .Index(t => t.CreneauID);
            
            DropTable("dbo.CreneauAdherents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CreneauAdherents",
                c => new
                    {
                        Creneau_Id = c.Int(nullable: false),
                        Adherent_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Creneau_Id, t.Adherent_Id });
            
            DropForeignKey("dbo.CreneauxAdherents", "CreneauID", "dbo.Creneaux");
            DropForeignKey("dbo.CreneauxAdherents", "AdherentID", "dbo.Adherents");
            DropIndex("dbo.CreneauxAdherents", new[] { "CreneauID" });
            DropIndex("dbo.CreneauxAdherents", new[] { "AdherentID" });
            DropTable("dbo.CreneauxAdherents");
            CreateIndex("dbo.CreneauAdherents", "Adherent_Id");
            CreateIndex("dbo.CreneauAdherents", "Creneau_Id");
            AddForeignKey("dbo.CreneauAdherents", "Adherent_Id", "dbo.Adherents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CreneauAdherents", "Creneau_Id", "dbo.Creneaux", "Id", cascadeDelete: true);
        }
    }
}
