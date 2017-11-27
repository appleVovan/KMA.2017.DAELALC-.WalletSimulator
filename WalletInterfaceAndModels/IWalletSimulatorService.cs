using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using WalletSimulator.Interface.Models;


namespace WalletSimulator.Interface
{
    [ServiceContract]
    public interface  IWalletSimulatorService
    {
        [OperationContract]
        bool UserExists(string login);
        [OperationContract]
        User GetUserByLogin(string login);
        [OperationContract]
        void AddUser(User user);
        [OperationContract]
        void AddWallet(Wallet wallet);
        [OperationContract]
        void DeleteTransaction(Transaction transaction);
        [OperationContract]
        void SaveWallet(Wallet wallet);
        [OperationContract]
        void AddTransaction(Transaction transaction);
        [OperationContract]
        List<User> GetAllUsers(Guid walletGuid);

        [OperationContract]
        void AddUserWalletRelation(UserWalletRelation userWallet);
        [OperationContract]
        void DeleteUserWalletRelation(UserWalletRelation userWallet);
        [OperationContract]
        List<UserWalletRelation> GetAssignedUsers(Guid currentWalletGuid);
        [OperationContract]
        List<Transaction> GetTransactions(Guid currentWalletGuid);
        [OperationContract]
        void DeleteWallet(Wallet selectedWallet);
    }
}
