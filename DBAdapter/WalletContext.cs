using System.Data.Entity;
using WalletInterfaceAndModels.Models;
using WalletSimulator.DBAdapter.Migrations;
using WalletSimulator.Interface.Models;

namespace WalletSimulator.DBAdapter
{
    public class WalletContext:DbContext
    {
        public WalletContext():base("UI")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WalletContext, Configuration>("UI"));

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserEntityConfiguration());
            modelBuilder.Configurations.Add(new Wallet.WalletEntityConfiguration());
            modelBuilder.Configurations.Add(new Transaction.TransactionEntityConfiguration());
            modelBuilder.Configurations.Add(new UserWalletRelation.UserWalletRelationEntityConfiguration());
        }
    }
}
