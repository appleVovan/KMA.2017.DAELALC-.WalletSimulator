using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LoginProject;
using LoginProject.Annotations;
using Prism.Commands;
using WalletSimulator.Interface;
using WalletSimulator.Interface.Models;
using WalletSimulator.ViewModels.Authentication;

namespace WalletSimulator.ViewModels
{
    class WalletConfigurationViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly Wallet _currentWallet;
        private bool _needSave;
        private ObservableCollection<User> _assignedUsers;
        private ObservableCollection<User> _notAssignedUsers;
        private ObservableCollection<Transaction> _transactions;
        private Transaction _selectedTransactions;
        private User _selectedNotAssignedUser;
        private User _selectedAssignedUser;
        private List<UserWalletRelation> _userWalletRelations;
        private Visibility _userVisibility= Visibility.Visible;
        private Visibility _transactionVisibility = Visibility.Hidden;
        private string _buttonName = "Go to Transactions";

        #endregion

        #region Properties
        #region Commands
        public DelegateCommand<KeyEventArgs> DeleteTransactionCommand { get; private set; }
        public RelayCommand NewTransactionCommand { get; }
        public RelayCommand SaveWalletCommand { get; }
        public RelayCommand AddUserCommand { get; }
        public RelayCommand DeleteUserCommand { get; }
        public RelayCommand GoToTransactionCommand { get; }
        
        #endregion

        public string ButtonName    
        {
            get { return _buttonName; }
            set
            {
                _buttonName = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value; 
                OnPropertyChanged();
            }
        }

        public Transaction SelectedTransaction
        {
            get { return _selectedTransactions; }
            set
            {
                _selectedTransactions = value;
                OnPropertyChanged();
            }
        }


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
        public long TotalOutCome
        {
            get { return _currentWallet.TotalOutcome; }
        }

        public Visibility ChangeControlUserVisibility
        {
            get { return _userVisibility; }
            set
            {
                _userVisibility = value;
                OnPropertyChanged();
            }
        }
        public Visibility ChangeControlTransactionVisibility
        {
            get { return _transactionVisibility; }
            set
            {
                _transactionVisibility = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> AssignedUsers
        {
            get { return _assignedUsers; }
            private set
            {
                _assignedUsers = value;
                OnPropertyChanged();
            }
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
            private set
            {
                _notAssignedUsers = value;
                OnPropertyChanged();
            }
        }
        #endregion

        

        #region Constructor
        public WalletConfigurationViewModel(Wallet wallet)
        {
            _currentWallet = wallet;
            LoadUsers();
            NewTransactionCommand = new RelayCommand(AddTransaction);
            DeleteTransactionCommand = new DelegateCommand<KeyEventArgs>(DeleteTransaction);
            AddUserCommand = new RelayCommand(AddUser, o=> SelectedNotAssignedUser!=null);
            DeleteUserCommand = new RelayCommand(DeleteUser, o=> SelectedAssignedUser !=null && SelectedAssignedUser.Guid != StationManager.CurrentUser.Guid);
            GoToTransactionCommand = new RelayCommand(GoToTransaction);
            SaveWalletCommand = new RelayCommand(SaveWallet, o => _needSave);
            PropertyChanged+= OnPropertyChanged;
        }

        private void GoToTransaction(object obj)
        {
            if (_userVisibility == Visibility.Visible)
            {
                ChangeControlUserVisibility = Visibility.Hidden;
                ChangeControlTransactionVisibility = Visibility.Visible;
                ButtonName = "Go to Users";
                LoadTransactions();
            }
            else
            {
                ChangeControlUserVisibility = Visibility.Visible;
                ChangeControlTransactionVisibility = Visibility.Hidden;
                ButtonName = "GO to Transactions";
                LoadUsers();                
            }
        }

        private void AddTransaction(Object o)
        {
            var transactionWindow = new Views.NewTransactionWindow(_currentWallet);
            transactionWindow.ShowDialog();
            
            LoadTransactions();
        }
        
        private void DeleteTransaction(KeyEventArgs args)
        {
            if (args.Key != Key.Delete) return;

            if (SelectedTransaction != null)
            {
                WalletServiceWrapper.DeleteTransaction(SelectedTransaction);
            }

            LoadTransactions();
        }

        private async void DeleteUser(object obj)
        {
            OnRequestLoader(true, false);
            await Task.Run(() =>
            {
                UserWalletRelation userWallet =
                    _userWalletRelations.FirstOrDefault(u => u.User.Guid == _selectedAssignedUser.Guid);
                _userWalletRelations.RemoveAll(u => u.User.Guid == _selectedAssignedUser.Guid);
                WalletServiceWrapper.DeleteUserWalletRelation(userWallet);
            });
            NotAssignedUsers.Add(SelectedAssignedUser);
            AssignedUsers.Remove(SelectedAssignedUser);
            OnRequestLoader(false, false);
        }

        private async void AddUser(object obj)
        {
            OnRequestLoader(true, false);
            await Task.Run(() =>
            {
                UserWalletRelation userWallet = new UserWalletRelation(SelectedNotAssignedUser, _currentWallet);
                WalletServiceWrapper.AddUserWalletRelation(userWallet);
                _userWalletRelations.Add(userWallet);
            });
            AssignedUsers.Add(SelectedNotAssignedUser);
            NotAssignedUsers.Remove(SelectedNotAssignedUser);
            OnRequestLoader(false, false);
        }


        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Title")
                _needSave = true;
        }


        #endregion

        private async void LoadUsers()
        {
            OnRequestLoader(true, false);
            await Task.Run(() =>
            {
                _userWalletRelations = WalletServiceWrapper.GetAssignedUsers(_currentWallet.Guid);
                AssignedUsers = new ObservableCollection<User>(_userWalletRelations.Select(relation => relation.User).ToList());
                NotAssignedUsers = new ObservableCollection<User>(WalletServiceWrapper.GetAllUsers(_currentWallet.Guid));
                SelectedAssignedUser = null;
                SelectedNotAssignedUser = null;
            });
            OnRequestLoader(false, false);
        }

        private async void LoadTransactions()
        {
            OnRequestLoader(true, false);
            await Task.Run(() =>
            {
                Transactions = new ObservableCollection<Transaction>(WalletServiceWrapper.GetTransactions(_currentWallet.Guid));
                SelectedTransaction = null;
            });
            OnRequestLoader(false, false);
        }

        private async void SaveWallet(object o)
        {
            OnRequestLoader(true, true);
            await Task.Run(() =>
            {
                WalletServiceWrapper.SaveWallet(_currentWallet);
                _needSave = false;
            });
            OnRequestLoader(false, true);
        }

        #region EventsAndHandlers
        
        #region ChangeVisibility
        internal event VisibilityHandler RequestVisibilityChange;
        internal delegate void VisibilityHandler(Visibility visibility);

        internal virtual void OnRequestVisibilityChange(Visibility visibility)
        {
            RequestVisibilityChange?.Invoke(visibility);
        }
        #endregion

        #region Loader
        internal event LoaderHandler RequestLoader;
        internal delegate void LoaderHandler(bool isShow, bool isMain);

        internal virtual void OnRequestLoader(bool isShow, bool isMain)
        {
            RequestLoader?.Invoke(isShow, isMain);
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion


    }
}
