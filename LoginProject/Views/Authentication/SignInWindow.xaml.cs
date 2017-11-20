using System.Windows;
using System.Windows.Controls;
using FontAwesome.WPF;
using LoginProject;
using WalletSimulator.ViewModels.Authentication;
using WalletSimulator.Views.Helpers;

namespace WalletSimulator.Views.Authentication
{
    internal partial class SignInWindow
    {
        #region Fields

        private ImageAwesome _loader;
        #endregion

        #region Constructor
        internal SignInWindow()
        {
            InitializeComponent();
            var signInViewModel = new SignInViewModel();
            signInViewModel.RequestClose += Close;
            signInViewModel.RequestLoader += OnRequestLoader;
            signInViewModel.RequestVisibilityChange += (x) => Visibility = x;
            DataContext = signInViewModel;
        }
        #endregion

        private void OnRequestLoader(bool isShow)
        {
            LoaderHelper.OnRequestLoader(MainGrid, ref _loader, isShow);
            IsEnabled = isShow;
        }

        #region EventHandlers
        private void Close(bool isQuitApp)
        {
            if (!isQuitApp)
                Close();
            else
                StationManager.ShutDown(0);
        } 
        #endregion
    }
}
