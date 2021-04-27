using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicePublisherConsole;
using System.ServiceModel;
using AuthenticatorServer;
using RestSharp;
using Newtonsoft.Json;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace ServicePublisherConsole
{
    /*
     * 
     * This is the client side service publisher
     * This is where a service will be published or unpublished for a user to access(WPF_Client)
     * 
     *  References : https://stackoverflow.com/questions/43698671/call-main-from-another-method
     */
    internal class Program
    {
        private static int token;
        static void Main(string[] args)
        {
            string option;

            Console.WriteLine("\n----------Welcome to Service Publisher Console----------");
            Console.WriteLine("\n1. Register");
            Console.WriteLine("2. Publish Service");
            Console.WriteLine("3. UnPublish Service");
            Console.WriteLine("\nEnter a number to continue...");

            option = Console.ReadLine();
            if (option.Equals("1"))
            {
                Console.Clear();
                Program.register();

            }
            else if (option.Equals("2"))
            {
                bool successful;
                Console.Clear();
                successful = Program.login();
                //if login successful call publishService() and after call main();
                if (successful) { Program.publishService(); Main(null); }

            }
            else if (option.Equals("3"))
            {
                bool successful;
                Console.Clear();
                successful = Program.login();
                //if login successful call unpublishService() and after call main();
                if (successful) { Program.unpublishService(); Main(null); }
            }
            else
            {
                Console.WriteLine("\nInvalid Option... Press ENTER to Continue to Main");
                Console.ReadLine();
                //Calling main function
                Main(null);
            }
        }

        static void register()
        {
            string userName;
            string password;
            string status;
            Console.WriteLine("\n\n----------Register to Service Publisher Console----------");
            Console.Write("\nEnter username :");
            userName = Console.ReadLine();
            Console.Write("\nEnter password :");
            password = Console.ReadLine();
            Console.Clear();

            //getting status of the registration process to display
            status = Models.AuthenticatorAccess.register(userName, password);

            Console.WriteLine($"\nUser Name : {userName}");
            Console.WriteLine($"Password : {password}");
            Console.WriteLine($"Status : {status}");

            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();

            //calling main function
            Main(null);
        }

        static bool login()
        {
            string userName;
            string password;
            Console.WriteLine("\n\n----------Login to Service Publisher----------");
            Console.Write("\nEnter username :");
            userName = Console.ReadLine();
            Console.Write("\nEnter password :");
            password = Console.ReadLine();

            //Assigning token value
            Program.token = Models.AuthenticatorAccess.login(userName, password);

            //returns positive means token is saved and login details are correct
            if (Program.token > 0)
            {
                Console.Clear();
                Console.WriteLine($"\nWelcome {userName}");
                Console.ReadLine();

                return true;
            }
            else if (Program.token == -1)//returns negative means token is not generated saved. Could be caused by invalid login credentials
            {
                Console.WriteLine($"\nUser Name : {userName}");
                Console.WriteLine($"Password : {password}");
                Console.WriteLine("Status : Invalid Login Credentials");
                Console.WriteLine("Press Enter to Continue...");
                Console.ReadLine();

                return false;
            }
            else
            {
                Console.WriteLine("Token Service Error");
                Console.ReadLine();

                return false;
            }
        }

        /*
         * Service Publishing input details are given
         * This function is where the interaction between the RegistryAccess class
         * The deserialization to display the response provided by the Registry
         * Display the Status of the service invocation
         * 
         */
        static void publishService()
        {
            IRestResponse response;
            Models.StatusDetailsIntermed status = new Models.StatusDetailsIntermed();

            Console.Clear();
            string serviceName, description, APIEndpoint, numberOfOperands, operandTypes;

            Console.WriteLine("\n----------Publish Service----------\n");

            Console.Write("\nEnter Service name  :");
            serviceName = Console.ReadLine();

            Console.Write("\nEnter description :");
            description = Console.ReadLine();

            Console.Write("\nEnter API endpoint :");
            APIEndpoint = Console.ReadLine();

            Console.Write("\nEnter number of operands :");
            numberOfOperands = Console.ReadLine();

            Console.Write("\nEnter operand types:");
            operandTypes = Console.ReadLine();

            //calling static Registry.publish()
            response = Models.RegistryAccess.publish(serviceName, description, APIEndpoint, numberOfOperands, operandTypes, Program.token);


            //De-serialize response
            status = JsonConvert.DeserializeObject<Models.StatusDetailsIntermed>(response.Content);

            Console.WriteLine($"Status : {status.Status}");
            Console.WriteLine($"Reason : {status.Reason}");

            Console.ReadLine();
        }

        /*
         * Service Publishing input details are given(the endpoint to unpublish)
         * This function is where the interaction between the RegistryAccess class
         * The deserialization to display the response provided by the Registry
         * Display the Status of the service invocation
         * 
         */

        static void unpublishService()
        {
            IRestResponse response;
            Models.StatusDetailsIntermed status = new Models.StatusDetailsIntermed();

            Console.Clear();
            string APIEndpoint;

            Console.WriteLine("\n----------Publish Service----------\n");

            Console.Write("\nEnter Service Endpoint  :");
            APIEndpoint = Console.ReadLine();
            
            //Fetching response
            response = Models.RegistryAccess.unpublish(APIEndpoint, Program.token);

            //De-serialize response to StatusDetailsIntermed object
            status = JsonConvert.DeserializeObject<Models.StatusDetailsIntermed>(response.Content);

            Console.WriteLine($"Status : {status.Status}");
            Console.WriteLine($"Reason : {status.Reason}");

            Console.ReadLine();
        }
    }
}
