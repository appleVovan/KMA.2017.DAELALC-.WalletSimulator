using System;
using System.Data.Entity;
using System.Linq;
using WalletSimulator.Interface.Models;

namespace WalletSimulator.DBAdapter
{
    public static class EntityWrapper
    {
        public static bool UserExists(string login)
        {
            using (var context = new WalletContext())
            {
                return context.Users.Any(u => u.Login == login);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var context = new WalletContext())
            {
                return context.Users.Include("UserWalletRelations.Wallet").FirstOrDefault(u => u.Login == login);
            }
        }

        public static void AddUser(User user)
        {
            using (var context = new WalletContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static void AddWallet(Wallet wallet)
        {
            using (var context = new WalletContext())
            {
                context.Wallets.Add(wallet);
                context.SaveChanges();
            }
        }

        public static void SaveWallet(Wallet wallet)
        {
            using (var context = new WalletContext())
            {
                context.Entry(wallet).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void AddTransaction(Transaction transaction)
        {
            using (var context = new WalletContext())
            {
                context.Transactions.Add(transaction);
                context.SaveChanges();
            }
        }
    }
}
