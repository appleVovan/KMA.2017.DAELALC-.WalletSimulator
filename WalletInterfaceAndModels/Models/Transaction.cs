using System;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace WalletSimulator.Interface.Models
{
    [DataContract]
    public class Transaction
    {
        #region Fields

        [DataMember] private Guid _guid;
        [DataMember] private int _amount;
        [DataMember] private string _title;
        [DataMember] private DateTime _date;
        [DataMember] private Guid _walletGuid;
        [DataMember] private Guid _userGuid;
        [DataMember] private Wallet _wallet;
        [DataMember] private User _user;

        #endregion

        #region Properties

        private Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        private int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Guid WalletGuid
        {
            get { return _walletGuid; }
            private set
            {
                _walletGuid = value;
                if (_wallet != null && _wallet.Guid != _walletGuid)
                {
                    _wallet = null;
                }
            }
        }

        internal Guid UserGuid
        {
            get { return _userGuid; }
            private set
            {
                _userGuid = value;
                if (_user != null && _user.Guid != _userGuid)
                {
                    _user = null;
                }
            }
        }

        internal Wallet Wallet
        {
            get { return _wallet; }
            private set
            {
                _wallet = value;
                if (_wallet.Guid != _walletGuid)
                    _walletGuid = _wallet.Guid;
            }
        }

        internal User User
        {
            get { return _user; }
            private set
            {
                _user = value;
                if (_user.Guid != _userGuid)
                    _userGuid = _user.Guid;
            }
        }

        #endregion

        #region Constructor

        public Transaction(int amount, string title, Wallet wallet, User user)
        {
            _guid = Guid.NewGuid();
            _amount = amount;
            _title = title;
            _date = DateTime.Today;

            Wallet = wallet;
            User = user;
            Wallet.Transactions.Add(this);
            User.Transactions.Add(this);
        }

        public Transaction()
        {

        }

        #endregion

        public void DeleteDatabaseValues()
        {
            _wallet = null;
            _user = null;
        }

        #region EntityFrameworkConfiguration

        public class TransactionEntityConfiguration : EntityTypeConfiguration<Transaction>
        {
            public TransactionEntityConfiguration()
            {
                ToTable("Transaction");
                HasKey(s => s.Guid);

                Property(p => p.Title)
                    .HasColumnName("Title")
                    .IsRequired();
                Property(p => p.Amount)
                    .HasColumnName("Amount")
                    .IsRequired();
                Property(p => p.Date)
                    .HasColumnName("Date")
                    .IsRequired();
                Property(p => p.UserGuid)
                    .HasColumnName("UserGuid")
                    .IsRequired();
                Property(p => p.WalletGuid)
                    .HasColumnName("WalletGuid")
                    .IsRequired();
            }
        }

        #endregion
        

        public override string ToString()
        {
            return Title + ": " + Amount;
        }
    }
}
