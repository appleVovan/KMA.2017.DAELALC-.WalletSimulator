using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using WalletInterfaceAndModels.Models;

namespace WalletSimulator.Interface.Models
{
    public class Wallet
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public long TotalIncome { get; set; }
        public long TotalOutcome { get; set; }

        public virtual List<UserWalletRelation> UserWalletRelations { get; set; }
        public virtual List<Transaction> Transactions { get; set; }

        public Wallet(string title, User user) : this()
        {
            Guid = Guid.NewGuid();
            this.Title = title;
            new UserWalletRelation(user, this);

        }

        public override string ToString()
        {
            return Title;
        }

        public Wallet()
        {
            this.Transactions = new List<Transaction>();
            this.UserWalletRelations = new List<UserWalletRelation>();
        }

        public class WalletEntityConfiguration : EntityTypeConfiguration<Wallet>
        {
            public WalletEntityConfiguration()
            {
                this.ToTable("Wallet");

                this.HasKey<Guid>(s => s.Guid);

                this.Property(p => p.Title)
                    .HasColumnName("Title")
                    .IsRequired();

                this.Ignore(s => s.TotalIncome);
                this.Ignore(s => s.TotalOutcome);

                HasMany(s => s.Transactions)
                    .WithRequired(w => w.Wallet)
                    .HasForeignKey(w => w.WalletGuid)
                    .WillCascadeOnDelete(true);

                HasMany(s=>s.UserWalletRelations).WithRequired(w=>w.Wallet).HasForeignKey(w=>w.WalletGuid).WillCascadeOnDelete(true);
            }
        }
    }   
}
