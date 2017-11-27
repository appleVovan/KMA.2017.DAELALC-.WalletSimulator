using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletSimulator.DBAdapter;
using WalletSimulator.Interface;
using WalletSimulator.Interface.Models;

namespace WalletSimulator.Service
{

    class WalletSimulatorService: IWalletSimulatorService
    {
        public bool UserExists(string login)
        {
            return EntityWrapper.UserExists(login);
        }

        public User GetUserByLogin(string login)
        {
            return EntityWrapper.GetUserByLogin(login);
        }

        public void AddUser(User user)
        {
            EntityWrapper.AddUser(user);
        }

        public void AddWallet(Wallet wallet)
        {
            EntityWrapper.AddWallet(wallet);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            EntityWrapper.DeleteTransaction(transaction);
        }

        public void AddTransaction(Transaction transaction)
        {
            EntityWrapper.AddTransaction(transaction);
        }

        public List<User> GetAllUsers(Guid walletGuid)
        {
            return EntityWrapper.GetAllUsers(walletGuid);
        }

        public void AddUserWalletRelation(UserWalletRelation userWallet)
        {
            EntityWrapper.AddUserWalletRelation(userWallet);
        }

        public void DeleteUserWalletRelation(UserWalletRelation userWallet)
        {
            EntityWrapper.DeleteUserWalletRelation(userWallet);
        }

        public List<UserWalletRelation> GetAssignedUsers(Guid currentWalletGuid)
        {
            return EntityWrapper.GetAssignedUsers(currentWalletGuid);
        }

        public List<Transaction> GetTransactions(Guid currentWalletGuid)
        {
            return EntityWrapper.GetTransactions(currentWalletGuid);
        }

        public void DeleteWallet(Wallet selectedWallet)
        {
            EntityWrapper.DeleteWallet(selectedWallet);
        }


        public void SaveWallet(Wallet wallet)
        {
            EntityWrapper.SaveWallet(wallet);
        }
    }
}
