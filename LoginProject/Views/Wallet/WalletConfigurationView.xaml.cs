using System;
using System.Windows.Controls;
using FontAwesome.WPF;
using WalletSimulator.ViewModels;
using WalletSimulator.Views.Helpers;

namespace WalletSimulator.Views.Wallet
{
    /// <summary>
    /// Interaction logic for WalletConfigurationView.xaml
    /// </summary>
    public partial class WalletConfigurationView : UserControl
    {

        private ImageAwesome _loader;
        public WalletConfigurationView(Interface.Models.Wallet wallet)
        {
            InitializeComponent();
            var walletModel = new WalletConfigurationViewModel(wallet);
            walletModel.RequestLoader+= WalletModelOnRequestLoader;
            DataContext = walletModel;
        }

        private void WalletModelOnRequestLoader(bool isShow, bool isMain)
        {
            LoaderHelper.OnRequestLoader(isMain?this.ControlGrid:ControlGrid, ref _loader, isShow);
        }
    }
}
