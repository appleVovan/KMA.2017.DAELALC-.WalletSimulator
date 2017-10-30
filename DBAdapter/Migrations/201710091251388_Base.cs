using System.Data.Entity.Migrations;

namespace WalletSimulator.DBAdapter.Migrations
{
    public partial class Base : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Guid = c.Guid(false),
                        Amount = c.Int(false),
                        Title = c.String(false),
                        WalletGuid = c.Guid(false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Wallet", t => t.WalletGuid, true)
                .Index(t => t.WalletGuid);
            
            CreateTable(
                "dbo.Wallet",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.UserGuid, cascadeDelete: true)
                .Index(t => t.UserGuid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UI = c.String(),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wallet", "UserGuid", "dbo.Users");
            DropForeignKey("dbo.Transaction", "WalletGuid", "dbo.Wallet");
            DropIndex("dbo.Wallet", new[] { "UserGuid" });
            DropIndex("dbo.Transaction", new[] { "WalletGuid" });
            DropTable("dbo.Users");
            DropTable("dbo.Wallet");
            DropTable("dbo.Transaction");
        }
    }
}
