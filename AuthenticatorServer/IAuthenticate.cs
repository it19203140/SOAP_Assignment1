using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace AuthenticatorServiceLibrary
{
    /*
     * 
     * Service Contract interface for the Authenticator
     * References: Lab 1
     *             ReadAllLines : https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalllines?view=net-5.0
     *             Creating a C# WCF Client_Server App in Visual Studio 2019 (720p) : https://www.youtube.com/watch?v=4GmQb0wN3TQ
     *             WCF Service Demo with Channel Factory on Client Side Part - 1 (720p) : https://www.youtube.com/watch?v=PyIyyrunxRo
     * 
     */

    [ServiceContract]
    public interface IAuthenticate
    {

         //@param name, password
         //@return message

        [OperationContract]
        string Register(String name, String password);


         //@param name, password
         //@return token int

        [OperationContract]
        int Login(String name, String password);


         //@param token
         //@return validation message

        [OperationContract]
        string validate(int token);
    }
}
