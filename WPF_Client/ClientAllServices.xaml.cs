using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
     * References :  **Most of the references we in the client GUI
     * 
     *               Navigation: https://www.youtube.com/watch?v=aBh0weP1bmo
     *                           https://www.youtube.com/watch?v=n0TOQj68oqo
     *                           https://stackoverflow.com/questions/50096839/redirect-to-another-page-in-wpf-on-certain-conditions
     *                           https://docs.microsoft.com/en-us/dotnet/api/system.windows.navigation.navigationwindow.navigate?view=net-5.0
     *               
     *               Data Grid and binding : https://docs.microsoft.com/en-us/answers/questions/93549/how-to-get-value-from-selecteditem-of-datagrid.html
     *                                       https://www.youtube.com/watch?v=1BIfpDZadmM
     *               
     *               Dynamic textbox generation :           https://stackoverflow.com/questions/2229019/how-to-dynamically-generate-a-textbox-control
     *               Add to stackplane                      https://www.codeproject.com/Questions/330268/Dynamic-creation-of-text-box-and-label-on-cutton-c
     *               Generate textbox name                  https://www.c-sharpcorner.com/Resources/756/how-to-create-a-textbox-in-wpf-dynamically.aspx
     *               Register and Unregister textbox name   https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.textbox?view=winrt-19041
     *               Remove Dynamic tex box             
     *               
     */
    public partial class ClientAllServices : Page
    {
        private static int TOKEN;
        private static int numberOfOperands;
        private ServiceDescriptionIntermed selectedService;

        private ServiceDescriptionNStatusInterMed getAllServices = new ServiceDescriptionNStatusInterMed();
        private ServiceProviderAccessObject accessObject = new ServiceProviderAccessObject();

        private ServiceProviderIntermed intermed = new ServiceProviderIntermed();

        public ClientAllServices(int token,string username)
        {
            //saving the token
            TOKEN = token;
            InitializeComponent();

            welcomeLabel.Content = $"Welcome {username}";
        }

        private void ViewAllServices_Click(object sender, RoutedEventArgs e)
        {
            //initalizing list
            getAllServices = null;

            DataTable allServices = new DataTable();

            //geting reponse for all services
            getAllServices = RegistryAccessClass.getAllServices(TOKEN);

            if(getAllServices != null)
            {
                //assigning to data grid
                allServicesDataGrid.DataContext = getAllServices.Services;
            }
            else
            {
                MessageBox.Show("Error! Unable to fetch all services!");
            }
        }
        
        private void SearchServices_Click(object sender, RoutedEventArgs e)
        {
            getAllServices = null;
            //get entered query
            String query = searchQueryTextBox.Text;

            //get reponse provided by the RegistryAccessClass.searchServices
            getAllServices = RegistryAccessClass.searchServices(query, TOKEN);

            //assign to the data grid
            allServicesDataGrid.DataContext = getAllServices.Services;
        }

        private void TestService_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxReply = "----Test----\n";

            string variables = "";

            try
            {
                int numberOfOperands = 0;
                numberOfOperands = int.Parse(selectedService.numberOfOperands);

                if (selectedService == null)
                {
                    MessageBox.Show("You have not selected a service to test");
                }
                else
                {
                    List<TextBox> textBoxes = new List<TextBox>();

                    //Creating textboxes dynamically based on the numberOfOperands in the selectedService
                    for (int x = 0; x < numberOfOperands; x++)
                    {
                        //Finding the registered name for the TextBox
                        TextBox txtNumber1 = (TextBox)this.stackPanelName.FindName($"no{x}");
                        //Add to a list of textBoxes
                        textBoxes.Add(txtNumber1);

                        //Remove the dynamically created textbox
                        this.stackPanelName.Children.Remove(txtNumber1);

                        //Unregister name in stack panel
                        stackPanelName.UnregisterName($"no{x}");
                    }

                    string serviceEndPoint = selectedService.serviceEndPoint;

                    //concatinating all the input values so that it will come in the form -> /1/2/3/ or so
                    foreach (TextBox textBox in textBoxes)
                    {
                        variables = variables+"/" + textBox.Text;
                    }

                    //serviceEndPoint = "localhost:8090/ADDTwoNumbers" + "/2/3/4" + "/" + "{token}"
                    serviceEndPoint = serviceEndPoint + variables + "/"+ TOKEN;

                    //MessageBox.Show(serviceEndPoint);

                    //Put the endpoint to accessObject of the Service Provider and fetch the object
                    intermed = accessObject.response(serviceEndPoint);

                    //Get a concatMessage with all the values 
                    messageBoxReply = ServiceProviderAccessObject.concatMessage(messageBoxReply, intermed);

                    //Display the service in a MessageBox
                    MessageBox.Show(messageBoxReply);

                    //Re-initilize if another service is invoked
                    intermed = null;
                    messageBoxReply = "";
                    serviceEndPoint = "";
                }
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("You have not selected a service to test");
            }


        }

        private void allServicesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int number;

            //if one service is selected from the DataGrid
            if (allServicesDataGrid.SelectedItems.Count == 1)
            {

                foreach (var obj in allServicesDataGrid.SelectedItems)
                {
                    selectedService = new ServiceDescriptionIntermed();

                    //get the object in the DataGrid
                    selectedService = obj as ServiceDescriptionIntermed;
                }

                try
                {
                    //get the number of operands needed by the service
                    number = int.Parse(selectedService.numberOfOperands);
                    numberOfOperands = number;


                    /*
                     * Create dynamic textboxes using a for loop based on the number of operands
                     * Assign a name for the TextBoxes to fetch entered data
                     * Register name in the StackPanel in the GUI
                     * Add the Textboxes
                     * 
                     */

                    for (int i = 0; i < number; i++)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Height = 50;
                        textBox.Width = 50;
                        textBox.Text = $"number{i}";
                        textBox.Name = $"no{i}";
                        stackPanelName.RegisterName(textBox.Name, textBox);
                        stackPanelName.Children.Add(textBox);
                    }

                    /*
                     * NOTE : Check the TestService_Click() to see how data from the dynamically objects are created and removed
                     * 
                     */

                }
                catch (FormatException)
                {
                    // Will catch System.FormatException if number of operands entered are invalid
                    MessageBox.Show("The number of operands are invalid");
                }
            }
        }
    }
}
