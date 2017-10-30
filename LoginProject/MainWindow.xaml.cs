using System;
using System.Windows;
using System.Windows.Controls;
using LoginProject;
using WalletSimulator.Authentication;
using WalletSimulator.DBAdapter;
using WalletSimulator.Interface.Models;

namespace WalletSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            foreach (var wallet in StationManager.CurrentUser.UserWalletRelations)
            {
                Button walletButton = new Button();
                WalletsPanel.Children.Add(walletButton);
                walletButton.Content = wallet.Wallet;
                walletButton.Click += WalletButton_Click;
            }
        }

        private void WalletButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Wallet wallet = new Wallet("WAllet", StationManager.CurrentUser);
            EntityWrapper.AddWallet(wallet);
        }
    }
}
