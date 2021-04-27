using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;
using ServicePublisherConsole;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace ServicePublisherConsole.Models
{
    /*
     * This is a static class that is used to create the connection with the Authenticator interface
     * This is used by the ServiceProvider to access the Authenticator
     * @returns interface which has which has created a channel for service invocation
     * 
     */
    internal static class AuthenticatorAccess
    {
        //Endpoint of Authenticator
        private static string URL = "net.tcp://localhost:8100/AuthenticationService";

        //Authenticator interface which will allow the user to access authenticator functions
        private static AuthenticatorServiceLibrary.IAuthenticate authenticatorAccessInterface;

        /*
         * @param both strings userName, password
         * @returns register status string
         * 
         */

        internal static string register(string userName, string password)
        {
            string status;

            //Interaction with channel factory class to access Authenticator services
            AuthenticatorInterfaceConn();

            status = authenticatorAccessInterface.Register(userName, password);

            return status;
        }

        /*
         * @param both strings userName, password
         * @returns token which will be used for validation
         * 
         */

        internal static int login(string userName, string password)
        {   
            //Interaction with channel factory class to access Authenticator services
            AuthenticatorInterfaceConn();

            return authenticatorAccessInterface.Login(userName, password);
        }

        /*
         * This function is used to connect to the Authenticator
         * channel is created
         * @returns void
         * 
         */
        internal static void AuthenticatorInterfaceConn()
        {
            ChannelFactory<AuthenticatorServiceLibrary.IAuthenticate> channel;
            NetTcpBinding binding = new NetTcpBinding();

            channel = new ChannelFactory<AuthenticatorServiceLibrary.IAuthenticate>(binding, URL);
            authenticatorAccessInterface = channel.CreateChannel();
        }
    }
}
