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
     *                         https://docs.microsoft.com/en-us/dotnet/api/system.windows.navigation.navigationwindow.navigate?view=net-5.0
     * 
     * 
     */
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new ClientLoginPage();
        }
    }
}
