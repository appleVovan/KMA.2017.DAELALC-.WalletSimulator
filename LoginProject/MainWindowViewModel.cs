using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LoginProject;
using LoginProject.Annotations;
using WalletSimulator.Interface;
using WalletSimulator.Interface.Models;

namespace WalletSimulator
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Wallet _selectedWallet;
        private readonly ObservableCollection<Wallet> _wallets;
        #endregion

        #region Properties
        #region Commands
        public RelayCommand AddWalletCommand { get; }
        #endregion

        public ObservableCollection<Wallet> Wallets
        {
            get { return _wallets; }
        }
        public Wallet SelectedWallet
        {
            get { return _selectedWallet; }
            set
            {
                _selectedWallet = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            _wallets = new ObservableCollection<Wallet>();
            foreach (var relation in StationManager.CurrentUser.UserWalletRelations)
            {
                _wallets.Add(relation.Wallet);
            }
            if (_wallets.Count>0)
                _selectedWallet = Wallets[0];
            AddWalletCommand = new RelayCommand(AddWallet);
            PropertyChanged+= OnPropertyChanged;
        }
        

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedWallet")
                OnWalletChanged(_selectedWallet);
        }

        #endregion

        private void AddWallet(object o)
        {
            Wallet wallet = new Wallet("New Wallet", StationManager.CurrentUser);
            WalletServiceWrapper.AddWallet(wallet);
            _wallets.Add(wallet);
            _selectedWallet = wallet;
        }
        
        #region EventsAndHandlers
        #region Loader
        internal event WalletChangedHandler WalletChanged;
        internal delegate void WalletChangedHandler(Wallet wallet);

        internal virtual void OnWalletChanged(Wallet wallet)
        {
            WalletChanged?.Invoke(wallet);
        }
        #endregion
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
