namespace ArtistManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        ArtistName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                        Email = c.String(),
                        MobileNo = c.String(),
                        Picture = c.String(),
                        MaritalStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ArtistId);
            
            CreateTable(
                "dbo.RoleEntries",
                c => new
                    {
                        RoleEntryID = c.Int(nullable: false, identity: true),
                        ArtistId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleEntryID)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleTitle = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleEntries", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleEntries", "ArtistId", "dbo.Artists");
            DropIndex("dbo.RoleEntries", new[] { "RoleId" });
            DropIndex("dbo.RoleEntries", new[] { "ArtistId" });
            DropTable("dbo.Roles");
            DropTable("dbo.RoleEntries");
            DropTable("dbo.Artists");
        }
    }
}
