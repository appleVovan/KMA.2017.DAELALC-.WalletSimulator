using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LoginProject;
using WalletSimulator.DBAdapter;
using WalletSimulator.Interface;
using WalletSimulator.Interface.Models;

namespace WalletSimulator
{
    /// <summary>
    /// Interaction logic for NewTransactionWindow.xaml
    /// </summary>
    public partial class NewTransactionWindow : Window
    {
        private readonly Wallet _currentWallet;

        public NewTransactionWindow(Wallet wallet)
        {
            InitializeComponent();
            _currentWallet = wallet;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction(Convert.ToInt32(TransactionAmount.Text), TransactionAmount.Text,
                _currentWallet, StationManager.CurrentUser);
            
            WalletServiceWrapper.AddTransaction(transaction);

            Close();
        }
    }
}
