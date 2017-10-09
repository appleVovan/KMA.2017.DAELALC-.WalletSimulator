using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WalletInterfaceAndModels.Models;

namespace LoginProject
{
    
    public class User
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UI { get; set; }

        public virtual List<Wallet> Wallets { get; set; }

        public User(string username, string password)
        {
            Guid = Guid.NewGuid();
            this.Password = password;
            this.Login = username;
        }

        public User()
        {
            
        }
        
        public class UserEntityConfiguration : EntityTypeConfiguration<User>
        {
            public UserEntityConfiguration()
            {
                this.ToTable("Users");

                this.HasKey<Guid>(s => s.Guid);

                this.Property(p => p.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired();

                this.Property(p => p.LastName)
                    .HasColumnName("LastName")
                    .IsRequired();

                this.Property(p => p.Email)
                    .HasColumnName("Email")
                    .IsOptional();

                this.Property(p => p.Login)
                    .HasColumnName("Login")
                    .IsRequired();

                this.Property(p => p.Password)
                    .HasColumnName("Password")
                    .IsRequired();

                //Ignore(p => p.UI);

                HasMany(s => s.Wallets)
                    .WithRequired(w => w.User)
                    .HasForeignKey(w => w.UserGuid)
                    .WillCascadeOnDelete(true);
            }
        }
    }
}
