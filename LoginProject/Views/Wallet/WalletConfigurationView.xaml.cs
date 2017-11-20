using System.Windows.Controls;
using WalletSimulator.ViewModels;

namespace WalletSimulator.Views.Wallet
{
    /// <summary>
    /// Interaction logic for WalletConfigurationView.xaml
    /// </summary>
    public partial class WalletConfigurationView : UserControl
    {
        public WalletConfigurationView(Interface.Models.Wallet wallet)
        {
            InitializeComponent();
            var walletModel = new WalletConfigurationViewModel(wallet);
            DataContext = walletModel;
        }
    }
}
