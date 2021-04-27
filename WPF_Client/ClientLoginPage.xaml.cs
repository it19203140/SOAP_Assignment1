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
using System.Windows.Navigation;
using System.Windows.Shapes;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace WPF_Client
{

    /*
     * 
     * References : Navigation : https://stackoverflow.com/questions/50096839/redirect-to-another-page-in-wpf-on-certain-conditions
     *                           https://docs.microsoft.com/en-us/dotnet/api/system.windows.navigation.navigationwindow.navigate?view=net-5.0
     *                           
     */


    public partial class ClientLoginPage : Page
    {
        public static int TOKEN;
        public ClientLoginPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = userNameTextBox.Text;
            string password = passwordTextBox.Text;

            TOKEN = AuthenticatorAccessClass.login(username, password);

            if (TOKEN > 0)
            {
                MessageBox.Show($"Welcome {username}");
                this.NavigationService.Navigate(new ClientAllServices(TOKEN,username));
            }
            else if (TOKEN == -1)
            {
                MessageBox.Show("Invalid Login details");
                MessageLabel.Content = "Re-enter Login Details or Register";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ClientRegisterPage());
        }
    }
}
