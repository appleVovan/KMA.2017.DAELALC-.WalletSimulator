using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LoginProject;
using LoginProject.Annotations;
using WalletSimulator.Interface;
using WalletSimulator.Interface.Models;

namespace WalletSimulator.ViewModels
{
    class WalletConfigurationViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly Wallet _currentWallet;
        private bool _needSave;
        private ObservableCollection<User> _assignedUsers;
        private ObservableCollection<User> _notAssignedUsers;
        private User _selectedNotAssignedUser;
        private User _selectedAssignedUser;

        #endregion

        #region Properties
        #region Commands
        
        public RelayCommand SaveWalletCommand { get; }
        public RelayCommand AddUserCommand { get; }
        public RelayCommand DeleteUserCommand { get; }
        public RelayCommand GoToTransactionCommand { get; }
        #endregion
        public string Title
        {
            get { return _currentWallet.Title; }
            set
            {
                _currentWallet.Title = value;
                _needSave = true;
                OnPropertyChanged();
            }
        }
        public long TotalIncome
        {
            get { return _currentWallet.TotalIncome; }
        }
        public long TotalExpences
        {
            get { return _currentWallet.TotalOutcome; }
        }
        public ObservableCollection<User> AssignedUsers
        {
            get { return _assignedUsers; }
        }

        public User SelectedAssignedUser    
        {
            get { return _selectedAssignedUser; }
            set
            {
                _selectedAssignedUser = value;
                OnPropertyChanged();
            }
        }

        public User SelectedNotAssignedUser 
        {
            get { return _selectedNotAssignedUser; }
            set
            {
                _selectedNotAssignedUser = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> NotAssignedUsers
        {
            get { return _notAssignedUsers; }
        }
        #endregion

        

        #region Constructor
        public WalletConfigurationViewModel(Wallet wallet)
        {
            _currentWallet = wallet;
            UpdateAssignedUserList();
            UpdateNotAssignedUserList();
            AddUserCommand = new RelayCommand(AddUser, o=> SelectedNotAssignedUser!=null);
            DeleteUserCommand = new RelayCommand(DeleteUser, o=> SelectedAssignedUser !=null);
            GoToTransactionCommand = new RelayCommand(GoToTransaction);
            SaveWalletCommand = new RelayCommand(SaveWallet, o => _needSave);
            PropertyChanged+= OnPropertyChanged;
        }

        private void GoToTransaction(object obj)
        {
            
        }

        private void DeleteUser(object obj)
        {
            UserWalletRelation userWallet =
                SelectedAssignedUser.UserWalletRelations.FirstOrDefault(u => u.WalletGuid == _currentWallet.Guid);

            SelectedAssignedUser.UserWalletRelations.RemoveAll(u => u.WalletGuid == _currentWallet.Guid);

            _currentWallet.UserWalletRelations.RemoveAll(u => u.WalletGuid == _currentWallet.Guid);
            WalletServiceWrapper.DeleteUserWalletRelation(userWallet);
            UpdateNotAssignedUserList();
            UpdateAssignedUserList();
        }

        private void AddUser(object obj)
        {
            UserWalletRelation userWallet = new UserWalletRelation(SelectedNotAssignedUser,_currentWallet);
            WalletServiceWrapper.AddUserWalletRelation(userWallet);
            UpdateNotAssignedUserList();
            UpdateAssignedUserList();

        }


        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Title")
                _needSave = true;
        }

        #endregion


        private void UpdateAssignedUserList()
        {
            _assignedUsers = new ObservableCollection<User>(_currentWallet.UserWalletRelations.Select(relation => relation.User));
            SelectedAssignedUser = null;
            OnPropertyChanged(nameof(AssignedUsers));
        }

        private void UpdateNotAssignedUserList()
        {
            _notAssignedUsers = new ObservableCollection<User>(WalletServiceWrapper.GetAllUsers(_currentWallet.Guid));
            SelectedNotAssignedUser = null;
            OnPropertyChanged(nameof(NotAssignedUsers));
        }



        private void SaveWallet(Object o)
        {
            WalletServiceWrapper.SaveWallet(_currentWallet);
            _needSave = false;
        }

        #region EventsAndHandlers
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }  
        #endregion
        #endregion


    }
}
