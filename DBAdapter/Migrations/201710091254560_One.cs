namespace DBAdapter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class One : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "UI");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UI", c => c.String());
        }
    }
}
