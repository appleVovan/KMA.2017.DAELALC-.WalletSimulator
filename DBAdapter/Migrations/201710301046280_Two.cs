namespace WalletSimulator.DBAdapter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Two : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wallet", "UserGuid", "dbo.Users");
            DropIndex("dbo.Wallet", new[] { "UserGuid" });
            CreateTable(
                "dbo.UserWalletRelation",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        WalletGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.UserGuid, cascadeDelete: true)
                .ForeignKey("dbo.Wallet", t => t.WalletGuid, cascadeDelete: true)
                .Index(t => t.UserGuid)
                .Index(t => t.WalletGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserWalletRelation", "WalletGuid", "dbo.Wallet");
            DropForeignKey("dbo.UserWalletRelation", "UserGuid", "dbo.Users");
            DropIndex("dbo.UserWalletRelation", new[] { "WalletGuid" });
            DropIndex("dbo.UserWalletRelation", new[] { "UserGuid" });
            DropTable("dbo.UserWalletRelation");
            CreateIndex("dbo.Wallet", "UserGuid");
            AddForeignKey("dbo.Wallet", "UserGuid", "dbo.Users", "Guid", cascadeDelete: true);
        }
    }
}
