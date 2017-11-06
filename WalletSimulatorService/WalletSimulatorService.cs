using System;
using System.Collections.Generic;
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

        public void AddTransaction(Transaction transaction)
        {
            EntityWrapper.AddTransaction(transaction);
        }

        public void SaveWallet(Wallet wallet)
        {
            EntityWrapper.SaveWallet(wallet);
        }
    }
}
