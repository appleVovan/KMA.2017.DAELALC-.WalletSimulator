using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LoginProject;
using LoginProject.Annotations;
using WalletSimulator.Interface;
using WalletSimulator.Interface.Models;

namespace WalletSimulator.ViewModels
{
    class WalletViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly Wallet _currentWallet;
        private bool _needSave;
        #endregion

        #region Properties
        #region Commands
        public RelayCommand NewTransactionCommand { get; }
        public RelayCommand SaveWalletCommand { get; }
        #endregion
        public string Title
        {
            get { return _currentWallet.Title; }
            set
            {
                _currentWallet.Title = value;
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
        public List<Transaction> Transactions
        {
            get { return _currentWallet.Transactions; }
        }
        #endregion

        #region Constructor
        public WalletViewModel(Wallet wallet)
        {
            _currentWallet = wallet;
            NewTransactionCommand = new RelayCommand(AddTransaction);
            SaveWalletCommand = new RelayCommand(SaveWallet, o => _needSave);
            PropertyChanged+= OnPropertyChanged;
        }
        

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Title")
                _needSave = true;
        }

        #endregion

        private void AddTransaction(Object o)
        {
            var transactionWindow = new Views.NewTransactionWindow(_currentWallet);
            transactionWindow.ShowDialog();

            OnPropertyChanged("Transactions");
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
