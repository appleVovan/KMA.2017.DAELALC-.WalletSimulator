using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WalletSimulator.Interface.Models;

namespace WalletSimulator.Interface
{
    public class WalletServiceWrapper
    {
        public static void AddTransaction(Transaction transaction)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                client.AddTransaction(transaction);
            }
        }

        public static bool UserExists(string login)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                return client.UserExists(login);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                return client.GetUserByLogin(login);
            }
        }

        public static void AddUser(User user)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                client.AddUser(user);
            }
        }

        public static void AddWallet(Wallet wallet)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                client.AddWallet(wallet);
            }
        }

        public static void SaveWallet(Wallet wallet)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                client.SaveWallet(wallet);
            }
        }
    }
}

