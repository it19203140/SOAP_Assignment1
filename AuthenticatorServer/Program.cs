using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AuthenticatorServiceLibrary;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace AuthenticatorServer
{
    class Program
    {
        /*
         * 
         * Main program where the service is hosted
         * This is where all the TCP/IP bindings occur
         * 
         * References: Lab 1
         *             ReadAllLines : https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalllines?view=net-5.0
         *             Creating a C# WCF Client_Server App in Visual Studio 2019 (720p) : https://www.youtube.com/watch?v=4GmQb0wN3TQ
         *             WCF Service Demo with Channel Factory on Client Side Part - 1 (720p) : https://www.youtube.com/watch?v=PyIyyrunxRo
         * 
         * 
         */


        static void Main(string[] args)
        {
            //Service end point
            Uri baseAddress = new Uri("net.tcp://localhost:8100/AuthenticationService");

            NetTcpBinding binding = new NetTcpBinding();

            ServiceHost serviceHost = new ServiceHost(typeof(Authenticate), baseAddress);
            
            serviceHost.AddServiceEndpoint(typeof(IAuthenticate), binding, baseAddress);
            serviceHost.Open();

            Console.WriteLine("Server is hosted !");
            Console.WriteLine("The Remote server is ready at " +baseAddress);
            Console.ReadLine();

            serviceHost.Close();
            
        }
    }
}
