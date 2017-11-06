namespace WalletSimulator.DBAdapter.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Base : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Amount = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        WalletGuid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.UserGuid, cascadeDelete: true)
                .ForeignKey("dbo.Wallet", t => t.WalletGuid, cascadeDelete: true)
                .Index(t => t.WalletGuid)
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
                        LastLoginDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
            CreateTable(
                "dbo.UserWalletRelation",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        WalletGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Wallet", t => t.WalletGuid, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserGuid, cascadeDelete: true)
                .Index(t => t.UserGuid)
                .Index(t => t.WalletGuid);
            
            CreateTable(
                "dbo.Wallet",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        TotalIncome = c.Long(nullable: false),
                        TotalOutcome = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserWalletRelation", "UserGuid", "dbo.Users");
            DropForeignKey("dbo.UserWalletRelation", "WalletGuid", "dbo.Wallet");
            DropForeignKey("dbo.Transaction", "WalletGuid", "dbo.Wallet");
            DropForeignKey("dbo.Transaction", "UserGuid", "dbo.Users");
            DropIndex("dbo.UserWalletRelation", new[] { "WalletGuid" });
            DropIndex("dbo.UserWalletRelation", new[] { "UserGuid" });
            DropIndex("dbo.Transaction", new[] { "UserGuid" });
            DropIndex("dbo.Transaction", new[] { "WalletGuid" });
            DropTable("dbo.Wallet");
            DropTable("dbo.UserWalletRelation");
            DropTable("dbo.Users");
            DropTable("dbo.Transaction");
        }
    }
}
