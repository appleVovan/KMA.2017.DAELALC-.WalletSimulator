using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletSimulator.Interface.Models;

namespace WalletInterfaceAndModels.Models
{
    public class Transaction
    {
        public Guid Guid { get; set; }
        public int Amount { get; set; }
        public string Title { get; set; }

        public Guid WalletGuid { get; set; }

        public Wallet Wallet { get; set; }

        public class TransactionEntityConfiguration : EntityTypeConfiguration<Transaction>
        {
            public TransactionEntityConfiguration()
            {
                this.ToTable("Transaction");

                this.HasKey<Guid>(s => s.Guid);

                this.Property(p => p.Title)
                    .HasColumnName("Title")
                    .IsRequired();

                this.Property(p => p.Amount)
                    .HasColumnName("Amount")
                    .IsRequired();

               
            }
        }
    }
}
