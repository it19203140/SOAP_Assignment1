using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticatorServiceLibrary;
using System.ServiceModel;

namespace AuthenticatorServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("net.tcp://localhost:8100/AuthenticationService");

            NetTcpBinding binding = new NetTcpBinding();

            using (ServiceHost serviceHost = new ServiceHost(typeof(Authenticate), baseAddress))
            {
                serviceHost.AddServiceEndpoint(typeof(IAuthenticate), binding, baseAddress);
                serviceHost.Open();

                Console.WriteLine($"The WCF server is ready at {baseAddress}.");
                Console.WriteLine("Press <ENTER> to terminate service...");
                Console.WriteLine();
                Console.ReadLine();
            }

        }
    }
}
