using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletInterfaceAndModels.Models;
using WalletSimulator.Tools;

namespace WalletSimulator.Interface.Models
{
    public class UserWalletRelation
    {
        #region Fields
        private Guid _guid;
        private Guid _userGuid;
        private Guid _walletGuid;

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

        internal Guid WalletGuid
        {
            get { return _walletGuid; }
            private set { _walletGuid = value; }
        }

       public virtual Wallet Wallet { get; set; }
        public virtual User User { get; set; }

        #endregion

        #region Constructor

        public UserWalletRelation(User user, Wallet wallet)
        {
            _guid = Guid.NewGuid();
            _userGuid = user.Guid;
            _walletGuid = wallet.Guid;
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
    }
}



