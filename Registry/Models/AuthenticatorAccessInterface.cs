using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using AuthenticatorServiceLibrary;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace Registry.Models
{
    /*
     * 
     * This is a static class that is used to create the connection with the Authenticator interface
     * @returns interface which has which has created a channel for service invocation
     * 
     * References : Lab 1
     */

    public static class AuthenticatorAccessInterface
    {
        public static IAuthenticate remoteConnection (){

            //Create an interface of the Authenticator
            IAuthenticate authenticate;

            ChannelFactory<IAuthenticate> channelFactory;
            NetTcpBinding tcp = new NetTcpBinding();

            //Endpoint of the authenticator service
            String URL = "net.tcp://localhost:8100/AuthenticationService";

            //bindings
            channelFactory = new ChannelFactory<IAuthenticate>(tcp, URL);

            //create channel for the interface and return it
            authenticate = channelFactory.CreateChannel();

            return authenticate;
        }
    }
}