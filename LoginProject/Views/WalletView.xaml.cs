using System.Windows.Controls;
using WalletSimulator.Interface.Models;
using WalletSimulator.ViewModels;

namespace WalletSimulator.Views
{
    /// <summary>
    /// Interaction logic for WalletView.xaml
    /// </summary>
    public partial class WalletView : UserControl
    {
        public WalletView(Interface.Models.Wallet wallet)
        {
            DataContext = new WalletViewModel(wallet);
            InitializeComponent();
        }
    }
}
