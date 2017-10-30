namespace WalletSimulator.DBAdapter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Three : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Wallet", "UserGuid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wallet", "UserGuid", c => c.Guid(nullable: false));
        }
    }
}
