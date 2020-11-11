namespace ProjetAssociationSports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adherents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResteaPayer = c.Double(nullable: false),
                        DateNaissance = c.DateTime(nullable: false),
                        NumTel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Creneaux",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionID = c.Int(nullable: false),
                        Jour = c.Int(nullable: false),
                        HeureDeb = c.Time(nullable: false, precision: 7),
                        HeureFin = c.Time(nullable: false, precision: 7),
                        NbrPlacesLimite = c.Int(nullable: false),
                        NbrPlaceRestantes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionID, cascadeDelete: true)
                .Index(t => t.SectionID);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisciplineID = c.Int(nullable: false),
                        Nom = c.String(),
                        Description = c.String(),
                        Prix = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplines", t => t.DisciplineID, cascadeDelete: true)
                .Index(t => t.DisciplineID);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CreneauAdherents",
                c => new
                    {
                        Creneau_Id = c.Int(nullable: false),
                        Adherent_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Creneau_Id, t.Adherent_Id })
                .ForeignKey("dbo.Creneaux", t => t.Creneau_Id, cascadeDelete: true)
                .ForeignKey("dbo.Adherents", t => t.Adherent_Id, cascadeDelete: true)
                .Index(t => t.Creneau_Id)
                .Index(t => t.Adherent_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Creneaux", "SectionID", "dbo.Sections");
            DropForeignKey("dbo.Sections", "DisciplineID", "dbo.Disciplines");
            DropForeignKey("dbo.CreneauAdherents", "Adherent_Id", "dbo.Adherents");
            DropForeignKey("dbo.CreneauAdherents", "Creneau_Id", "dbo.Creneaux");
            DropIndex("dbo.CreneauAdherents", new[] { "Adherent_Id" });
            DropIndex("dbo.CreneauAdherents", new[] { "Creneau_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Sections", new[] { "DisciplineID" });
            DropIndex("dbo.Creneaux", new[] { "SectionID" });
            DropTable("dbo.CreneauAdherents");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Disciplines");
            DropTable("dbo.Sections");
            DropTable("dbo.Creneaux");
            DropTable("dbo.Adherents");
        }
    }
}
