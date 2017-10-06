using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using LoginProject.Annotations;

namespace LoginProject
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly User _userCandidate;
        #endregion

        #region Properties
        #region Command
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand SignInCommand { get; private set; }
        public RelayCommand SignUpCommand { get; private set; } 
        #endregion

        internal string Password
        {
            get => _userCandidate.Password;
            set => _userCandidate.Password = value;
        }
        public string Username
        {
            get => _userCandidate.Username;
            set
            {
                _userCandidate.Username = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ConstructorAndInit
        public LoginViewModel(User userCandidate)
        {
            InitializeComands();
            _userCandidate = userCandidate;
        }

        private void InitializeComands()
        {
            CloseCommand = new RelayCommand(obj => OnRequestClose(true));
            SignInCommand = new RelayCommand(SignIn,
                o => !String.IsNullOrEmpty(Username) &&
                     !String.IsNullOrEmpty(Password));
            SignUpCommand = new RelayCommand(SignUp, o => !String.IsNullOrEmpty(Username) &&
                                                           !String.IsNullOrEmpty(Password));
        }
        #endregion
       
        private async void SignUp(object obj)
        {
            OnRequestLoader(true);
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
                if (DbAdapter.Users.Any(user => user.Username == Username))
                {
                    MessageBox.Show("User with this username already exists");
                    return;
                }
                DbAdapter.Users.Add(new User(Username, Password));
                MessageBox.Show("User successfully created");
            });
            OnRequestLoader(false);
        }
        
        private async void SignIn(object obj)
        {
            OnRequestLoader(true);
            var result = await Task.Run(() =>
            {
                Thread.Sleep(5000);
                var currentUser = DbAdapter.Users.FirstOrDefault(user => user.Username == Username &&
                                                                         user.Password == Password);
                if (currentUser == null)
                {
                    MessageBox.Show("Wrong Username or Password");
                    return false;
                }

                StationManager.CurrentUser = currentUser;
                MessageBox.Show("You have entered just now");
                return true;
            });
            OnRequestLoader(false);
            if (result)
                OnRequestClose(false);
        }

        #region EventsAndHandlers
        #region Close
        internal event CloseHandler RequestClose;
        public delegate void CloseHandler(bool isQuitApp);

        protected virtual void OnRequestClose(bool isquitapp)
        {
            RequestClose?.Invoke(isquitapp);
        }
        #endregion

        #region Loader
        internal event LoaderHandler RequestLoader;
        public delegate void LoaderHandler(bool isShow);

        protected virtual void OnRequestLoader(bool isShow)
        {
            RequestLoader?.Invoke(isShow);
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
        #endregion
    }
}
