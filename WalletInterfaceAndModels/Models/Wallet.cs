using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginProject;

namespace WalletInterfaceAndModels.Models
{
    public class Wallet
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public long TotalIncome { get; set; }
        public long TotalOutcome { get; set; }
        public Guid UserGuid { get; set; }

        public User User { get; set; }
        public virtual List<Transaction> Transactions { get; set; }

        public Wallet(string username, string password)
        {
            Guid = Guid.NewGuid();
        }

        public Wallet()
        {

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
            }
        }
    }   
}
