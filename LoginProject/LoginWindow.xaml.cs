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
using static System.String;

namespace LoginProject
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if(IsNullOrEmpty(Username.Text) || IsNullOrEmpty(Password.Password))
            {
                MessageBox.Show("Username or Password not set");
                return;
            }
            if (DbAdapter.Users.Any(user => user.Username == Username.Text))
            {
                MessageBox.Show("User with this username already exists");
                return;
            }
            DbAdapter.Users.Add(new User(Username.Text, Password.Password));
            MessageBox.Show("User successfully created");
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (IsNullOrEmpty(Username.Text) || IsNullOrEmpty(Password.Password))
            {
                MessageBox.Show("Username or Password not set");
                return;
            }
            var currentUser = DbAdapter.Users.FirstOrDefault(user => user.Username == Username.Text &&
                                                                     user.Password == Password.Password);
            if (currentUser == null)
            {
                MessageBox.Show("Wrong Username or Password");
                return;
            }

            StationManager.CurrentUser = currentUser;
            MessageBox.Show("You have entered just now");
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
