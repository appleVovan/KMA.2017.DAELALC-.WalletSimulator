using System.Windows;
using System.Windows.Controls;
using WalletSimulator.Interface.Models;
using WalletSimulator.ViewModels;
using WalletSimulator.Views;
using WalletSimulator.Views.Wallet;
using SignInWindow = WalletSimulator.Views.Authentication.SignInWindow;

namespace WalletSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel;
        private WalletConfigurationView _currentWalletConfigurationView;

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
            _mainWindowViewModel = new MainWindowViewModel();
            _mainWindowViewModel.WalletChanged+= OnWalletChanged;
            DataContext = _mainWindowViewModel;
        }

        private void OnWalletChanged(Wallet wallet)
        {
            if (_currentWalletConfigurationView == null)
            {
                _currentWalletConfigurationView = new WalletConfigurationView(wallet);
                MainGrid.Children.Add(_currentWalletConfigurationView);
                Grid.SetRow(_currentWalletConfigurationView, 0);
                Grid.SetRowSpan(_currentWalletConfigurationView, 2);
                Grid.SetColumn(_currentWalletConfigurationView, 1);
            }
            else
                _currentWalletConfigurationView = new WalletConfigurationView(wallet);

        }
        
    }
}
