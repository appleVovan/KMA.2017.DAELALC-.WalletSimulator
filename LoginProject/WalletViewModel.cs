using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LoginProject;
using LoginProject.Annotations;
using WalletInterfaceAndModels.Models;
using WalletSimulator.Interface.Models;

namespace WalletSimulator
{
    class WalletViewModel : INotifyPropertyChanged
    {
        private Wallet currentWallet;
        private string _title;
        private long _totalIncome;
        private long _totalExpences;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value; 
                OnPropertyChanged();
            }
        }

        public long TotalIncome
        {
            get { return _totalIncome; }
            set
            {
                _totalIncome = value;
                OnPropertyChanged();
            }
        }

        public long TotalExpences
        {
            get { return _totalExpences; }
            set
            {
                _totalExpences = value;
                OnPropertyChanged();
            }
        }

        public List<Transaction> Transactions
        {
            get { return currentWallet.Transactions; }
        }

        WalletViewModel(Wallet wallet)
        {
            currentWallet = wallet;
            NewTransaction = new RelayCommand(AddTransaction);
        }

        private void AddTransaction(Object o)
        {
            
        }

        public RelayCommand NewTransaction { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
