using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace WPF_Client
{
    /*
     * This is a static class that is used to create the connection with the Authenticator interface
     * This is used by the WPF_Client to access the Authenticator
     * @returns interface which has which has created a channel for service invocation
     * 
     * References : Lab 1
     */
    public static class AuthenticatorAccessClass
    {
        //Authenticator End Point to consumption
        private static string URL = "net.tcp://localhost:8100/AuthenticationService";
        private static AuthenticatorServiceLibrary.IAuthenticate authenticatorAccessInterface;

        internal static int login(string username, string password)
        {

            //Creating connection to Authenicator services
            AuthenticatorInterfaceConn();

            return authenticatorAccessInterface.Login(username, password);
        }

        internal static string register(string username, string password)
        {

            //Creating connection to Authenicator services
            AuthenticatorInterfaceConn();

            return authenticatorAccessInterface.Register(username, password);
        }

        /*
         * Function where the channel factory instance is created, binded and assigned to a interface
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
