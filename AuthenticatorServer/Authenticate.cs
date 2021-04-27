using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using SOA_Utilities;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace AuthenticatorServiceLibrary
{


    /*
     * 
     * This class has the implementation of IAuthenticator
     * Also include the implementation of a FileHandling as an internal class
     * References: Lab 1
     *             ReadAllLines : https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalllines?view=net-5.0
     *             Creating a C# WCF Client_Server App in Visual Studio 2019 (720p) : https://www.youtube.com/watch?v=4GmQb0wN3TQ
     *             WCF Service Demo with Channel Factory on Client Side Part - 1 (720p) : https://www.youtube.com/watch?v=PyIyyrunxRo
     * 
     */


    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = true)]
    internal class Authenticate : IAuthenticate
    {


        /*
         * 
         * Register function that registers user
         * Return response message -> if user registered successfully -> @returns "Successfully Registered"
         *                         -> if user not successful -> @returns "Unsuccessful Registration"
         * 
         */


        public string Register(String name, String password)
        {
            String loginAndReg = name + "\t" + password;
            SOA_Utilities.FileHandling.writeDataToTextFile(loginAndReg, SOA_Utilities.Constants.pathForRegistrationText, SOA_Utilities.Constants.REG);

            //Checking if user is registered
            if (File.ReadAllLines(SOA_Utilities.Constants.pathForRegistrationText).Contains(loginAndReg))
            {
                return "Successfully Registered";
            }
            else
            {
                return "Unsuccessful Registration";
            }

        }


        /*
         * 
         * Login function validates all the component
         * Return a postive token value if Login() successful
         * Return a negative if unsuccessful
         *
         */


        public int Login(String name, String password)
        {
            Random tokengen = new Random();
            String details = name + "\t" + password;


            //Writing to text file
            if (File.ReadAllLines(SOA_Utilities.Constants.pathForRegistrationText).Contains(details))
            {

                int token = tokengen.Next(1000000,1000000000);
                SOA_Utilities.FileHandling.writeDataToTextFile(token.ToString(), Constants.pathForTokenText, SOA_Utilities.Constants.TOKEN);
                //return random positive token if match is found
                return token;
            }
            else
            { 
                //return negative integer as unsuccessfull
                return -1;
            }

        }

        public string validate(int token)
        {
            String returnMessage = "";

            if (File.ReadAllLines(SOA_Utilities.Constants.pathForTokenText).Contains(token.ToString()))
            {
                //if string exists in text
                returnMessage = "validated";
            }
            else
            {
                //if string does not exist in path
                returnMessage = "not validated";
            }

            return returnMessage;
        }
    }
}


