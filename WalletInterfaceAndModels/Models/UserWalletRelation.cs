using System;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace WalletSimulator.Interface.Models
{
    [DataContract(IsReference = true)]
    public class UserWalletRelation
    {
        #region Fields
        [DataMember]
        private Guid _guid;
        [DataMember]
        private Guid _userGuid;
        [DataMember]
        private Guid _walletGuid;
        [DataMember]
        private Wallet _wallet;
        [DataMember]
        private User _user;

        #endregion

        #region Properties
        private Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }
        internal Guid UserGuid
        {
            get { return _userGuid; }
            private set { _userGuid = value; }
        }
        public Guid WalletGuid
        {
            get { return _walletGuid; }
            private set { _walletGuid = value; }
        }

        public Wallet Wallet
        {
            get { return _wallet; }
            private set { _wallet = value; }
        }
        public User User
        {
            get { return _user; }
            private set { _user = value; }
        }
        #endregion

        #region Constructor
        public UserWalletRelation(User user, Wallet wallet)
        {
            _guid = Guid.NewGuid();
            _userGuid = user.Guid;
            _walletGuid = wallet.Guid;
            User = user;
            Wallet = wallet;
            user.UserWalletRelations.Add(this);
            wallet.UserWalletRelations.Add(this);
        }
        private UserWalletRelation()
        {
        }
        #endregion
        
        #region EntityConfiguration
        public class UserWalletRelationEntityConfiguration : EntityTypeConfiguration<UserWalletRelation>
        {
            public UserWalletRelationEntityConfiguration()
            {
                ToTable("UserWalletRelation");
                HasKey(s => s.Guid);

                Property(p => p.UserGuid)
                    .HasColumnName("UserGuid")
                    .IsRequired();
                Property(p => p.WalletGuid)
                    .HasColumnName("WalletGuid")
                    .IsRequired();
            }
        }
        #endregion

        public void DeleteDatabaseValues()
        {
            User = null;
            Wallet = null;
        }
    }
}



