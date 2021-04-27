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

namespace ServiceProvider.Models
{
    /*
     * This is a static class that is used to create the connection with the Authenticator interface
     * @returns interface which has which has created a channel for service invocation
     * 
     *     References : Lab 1
     * 
     */
    public static class AuthenticatorAccessInterface
    {
        public static IAuthenticate remoteConnection (){

            //Interface of the authenticator
            IAuthenticate authenticate;
            ChannelFactory<IAuthenticate> channelFactory;

            //Binding through tcp
            NetTcpBinding tcp = new NetTcpBinding();

            //Endpoint to access authenticator
            String URL = "net.tcp://localhost:8100/AuthenticationService";

            //Creating channelFactory intance for authenticator
            channelFactory = new ChannelFactory<IAuthenticate>(tcp, URL);

            //Create channel and assign to authenticator interface
            authenticate = channelFactory.CreateChannel();

            return authenticate;
        }
    }
}