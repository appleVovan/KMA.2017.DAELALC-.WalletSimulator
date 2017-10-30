using System.Data.Entity.Migrations;

namespace WalletSimulator.DBAdapter.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WalletContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DBAdapter.WalletContext";
        }

        protected override void Seed(WalletContext context)
        {
        }
    }
}
