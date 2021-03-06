﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using WalletSimulator.Interface.Models;

namespace WalletSimulator.Interface
{
    public class WalletServiceWrapper
    {
        public static void DeleteTransaction(Transaction transaction)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                client.DeleteTransaction(transaction);
            }
        }

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

        public static List<User> GetAllUsers(Guid walletGuid)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                return client.GetAllUsers(walletGuid);
            }
        }

        public static void AddUserWalletRelation(UserWalletRelation userWallet)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                client.AddUserWalletRelation(userWallet);
            }
        }

        public static void DeleteUserWalletRelation(UserWalletRelation userWallet)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                client.DeleteUserWalletRelation(userWallet);
            }
        }

        public static List<UserWalletRelation> GetAssignedUsers(Guid currentWalletGuid)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                return client.GetAssignedUsers(currentWalletGuid);
            }
        }

        public static List<Transaction> GetTransactions(Guid currentWalletGuid)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                return client.GetTransactions(currentWalletGuid);
            }
        }

        public static void DeleteWallet(Wallet selectedWallet)
        {
            using (var myChannelFactory = new ChannelFactory<IWalletSimulatorService>("Server"))
            {
                IWalletSimulatorService client = myChannelFactory.CreateChannel();
                client.DeleteWallet(selectedWallet);
            }
        }
    }
}

