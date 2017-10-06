using System.Windows;
using System.Windows.Controls;
using FontAwesome.WPF;

namespace LoginProject
{
  
    public partial class LoginWindow
    {
        #region Fields
        private readonly LoginViewModel _loginViewModel;
        private ImageAwesome _loader;
        #endregion

        #region Constructor
        public LoginWindow()
        {
            InitializeComponent();
            _loginViewModel = new LoginViewModel(new User());
            _loginViewModel.RequestClose += Close;
            _loginViewModel.RequestLoader += LoginViewModelOnRequestLoader;
            DataContext = _loginViewModel;
        }

        private void LoginViewModelOnRequestLoader(bool isShow)
        {
            if (isShow && _loader==null)
            {
                _loader = new ImageAwesome();
                MainGrid.Children.Add(_loader);
                _loader.Icon = FontAwesomeIcon.Refresh;
                _loader.Spin = true;
                Grid.SetRow(_loader, 1);
                Grid.SetColumn(_loader, 1);
                IsEnabled = false;
            }
            else if (_loader!=null)
            {
                MainGrid.Children.Remove(_loader);
                _loader = null;
                IsEnabled = true;
            }
        }

        #endregion

        #region EventHandlers
        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            _loginViewModel.Password = Password.Password;
        }

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
