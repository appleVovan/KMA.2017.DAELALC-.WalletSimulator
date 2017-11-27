using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using LoginProject;
using LoginProject.Annotations;
using Prism.Commands;
using WalletSimulator.Interface;
using WalletSimulator.Interface.Models;

namespace WalletSimulator
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Wallet _selectedWallet;
        private ObservableCollection<Wallet> _wallets;
        #endregion

        #region Properties
        #region Commands
        public RelayCommand AddWalletCommand { get; }
        public DelegateCommand<KeyEventArgs> DeleteWalletCommand { get; private set; }
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
            FillWallets();
            AddWalletCommand = new RelayCommand(AddWallet);
            DeleteWalletCommand = new DelegateCommand<KeyEventArgs>(DeleteWallet);
            PropertyChanged += OnPropertyChanged;
        }
        

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedWallet")
                OnWalletChanged(_selectedWallet);
        }

        #endregion
        private void FillWallets()
        {
            _wallets = new ObservableCollection<Wallet>();
            foreach (var relation in StationManager.CurrentUser.UserWalletRelations)
            {
                _wallets.Add(relation.Wallet);
            }
            if (_wallets.Count > 0)
            {
                _selectedWallet = Wallets[0];
            }
        }

        private void DeleteWallet(KeyEventArgs args)
        {
            if (args.Key != Key.Delete) return;

            if (SelectedWallet == null) return;

            StationManager.CurrentUser.UserWalletRelations.RemoveAll(uwr => uwr.WalletGuid == SelectedWallet.Guid);
            WalletServiceWrapper.DeleteWallet(SelectedWallet);
            FillWallets();
            OnPropertyChanged(nameof(SelectedWallet));
            OnPropertyChanged(nameof(Wallets));
        }

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
