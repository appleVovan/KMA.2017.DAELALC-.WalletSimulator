using System;
using System.Windows;
using LoginProject;
using WalletSimulator.Interface;
using WalletSimulator.Interface.Models;

namespace WalletSimulator.Views
{
    /// <summary>
    /// Interaction logic for NewTransactionWindow.xaml
    /// </summary>
    public partial class NewTransactionWindow : Window
    {
        private readonly Interface.Models.Wallet _currentWallet;

        public NewTransactionWindow(Interface.Models.Wallet wallet)
        {
            InitializeComponent();
            _currentWallet = wallet;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction(Convert.ToInt32(TransactionAmount.Text), TransactionTitle.Text,
                _currentWallet, StationManager.CurrentUser);
            
            WalletServiceWrapper.AddTransaction(transaction);

            Close();
        }
    }
}
