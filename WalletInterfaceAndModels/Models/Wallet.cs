using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace WalletSimulator.Interface.Models
{
    [DataContract(IsReference = true)]
    public class Wallet
    {
        #region Fields
        [DataMember]
        private Guid _guid;
        [DataMember]
        private string _title;
        [DataMember]
        private long _totalIncome;
        [DataMember]
        private long _totalOutcome;
        [DataMember]
        private List<UserWalletRelation> _userWalletRelations;
        [DataMember]
        private List<Transaction> _transactions;
        #endregion

        #region Properties
        internal Guid Guid
        {
            get { return _guid; }
            private set { _guid = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public long TotalIncome
        {
            get { return _totalIncome; }
            private set { _totalIncome = value; }
        }
        public long TotalOutcome
        {
            get { return _totalOutcome; }
            private set { _totalOutcome = value; }
        }

        internal List<UserWalletRelation> UserWalletRelations
        {
            get { return _userWalletRelations; }
            private set { _userWalletRelations = value; }
        }

        public List<Transaction> Transactions
        {
            get { return _transactions; }
            private set { _transactions = value; }
        }
        #endregion

        #region Constructor
        public Wallet(string title, User user) : this()
        {
            _guid = Guid.NewGuid();
            _title = title;
            _totalIncome = 0;
            _totalOutcome = 0;
            new UserWalletRelation(user, this);
        }
        private Wallet()
        {
            _transactions = new List<Transaction>();
            _userWalletRelations = new List<UserWalletRelation>();
        } 
        #endregion

        public override string ToString()
        {
            return Title;
        }

        #region EntityFrameworkConfiguration
        public class WalletEntityConfiguration : EntityTypeConfiguration<Wallet>
        {
            public WalletEntityConfiguration()
            {
                ToTable("Wallet");
                HasKey(s => s.Guid);

                Property(p => p.Title)
                    .HasColumnName("Title")
                    .IsRequired();
                Property(s => s.TotalIncome)
                    .HasColumnName("TotalIncome")
                    .IsRequired();
                Property(s => s.TotalOutcome)
                    .HasColumnName("TotalOutcome")
                    .IsRequired();

                HasMany(s => s.Transactions)
                    .WithRequired(w => w.Wallet)
                    .HasForeignKey(w => w.WalletGuid)
                    .WillCascadeOnDelete(true);
                HasMany(s => s.UserWalletRelations)
                    .WithRequired(w => w.Wallet)
                    .HasForeignKey(w => w.WalletGuid)
                    .WillCascadeOnDelete(true);
            }
        } 
        #endregion
    }   
}
