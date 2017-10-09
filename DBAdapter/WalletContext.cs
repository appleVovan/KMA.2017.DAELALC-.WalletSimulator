using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAdapter.Migrations;
using LoginProject;
using WalletInterfaceAndModels.Models;

namespace DBAdapter
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
        }
    }
}
