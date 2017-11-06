using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LoginProject;
using WalletSimulator.Authentication;
using WalletSimulator.DBAdapter;
using WalletSimulator.Interface;
using WalletSimulator.Interface.Models;

namespace WalletSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Wallet _currentWallet;
        private WalletView _currentWalletView;
        private Button _currentButton;
        private Brush _defaultBorderBrush;
        public MainWindow()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;
            SignInWindow loginWindow = new SignInWindow();

            loginWindow.Closed += (sender, args) => Initialize();
            loginWindow.ShowDialog();
            
            
        }

        private void Initialize()
        {
            Visibility = Visibility.Visible;
            FillWallet();
            
        }

        private void OnExit(object obj, EventArgs a)
        {
            MessageBox.Show("Salut!");
            Environment.Exit(0);
        }

        private void FillWallet()
        {
            WalletsPanel.Children.Clear();
            foreach (var wallet in StationManager.CurrentUser.UserWalletRelations)
            {
                if (_currentWallet == null)
                    _currentWallet = wallet.Wallet;
                Button walletButton = new Button();
                WalletsPanel.Children.Add(walletButton);
                walletButton.Content = wallet.Wallet;
                walletButton.Click += WalletButton_Click;
                if (wallet.Wallet == _currentWallet)
                {
                    if (_defaultBorderBrush == null)
                        _defaultBorderBrush = walletButton.BorderBrush;
                    walletButton.BorderBrush = new SolidColorBrush(Colors.Red);
                    _currentButton = walletButton;
                }
            }
            if (_currentWallet == null)
                return;
            if (_currentWalletView == null)
            {
                _currentWalletView = new WalletView(_currentWallet);
                MainGrid.Children.Add(_currentWalletView);
                Grid.SetRow(_currentWalletView, 1);
                Grid.SetColumn(_currentWalletView, 1);
            }
            else
            {
                _currentWalletView.DataContext = new WalletViewModel(_currentWallet);
            }
        }

        private void WalletButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var wallet = button.Content as Wallet;
            _currentWallet = wallet;
            _currentButton.BorderBrush = _defaultBorderBrush;
            _currentButton = button;
            _currentButton.BorderBrush = new SolidColorBrush(Colors.Red);
            _currentWalletView.DataContext = new WalletViewModel(wallet);
        }

        private void AddWallet_Click(object sender, RoutedEventArgs e)
        {
            Wallet wallet = new Wallet("WAllet", StationManager.CurrentUser);
            WalletServiceWrapper.AddWallet(wallet);
            FillWallet();
        }
    }
}
