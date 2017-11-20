using System.Windows;
using System.Windows.Controls;
using FontAwesome.WPF;
using WalletSimulator.ViewModels.Authentication;
using WalletSimulator.Views.Helpers;

namespace WalletSimulator.Views.Authentication
{  
    internal partial class SignUpWindow
    {
        #region Fields
        private ImageAwesome _loader;
        #endregion

        #region Constructor
        internal SignUpWindow()
        {
            InitializeComponent();
            var signUpViewModel = new SignUpViewModel();
            signUpViewModel.RequestClose += Close;
            signUpViewModel.RequestLoader += OnRequestLoader;
            DataContext = signUpViewModel;
        }

        #endregion
        private void OnRequestLoader(bool isShow)
        {
            LoaderHelper.OnRequestLoader(MainGrid, ref _loader, isShow);
            IsEnabled = isShow;
        }
    }
}
