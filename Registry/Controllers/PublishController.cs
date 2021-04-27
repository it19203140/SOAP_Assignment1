using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestSharp;
using Newtonsoft.Json;
using Registry;
using System.IO;
using SOA_Utilities;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace Registry.Controllers
{
    /*
     * 
     * Controller class that publish service details
     * @param is in JSON format(ServiceDescription json string and a token)
     * @returns in StatusDetails object
     * if token not validated -> @returns -> 
     *                                          Reason = "Authentication Error";
                                                Status = "Denied";

     * token validated && service published -> @returns ->
     *                                          Status = "Published !";
                                                Reason = "Service Registered !";

     * token validated but service not published -> @returns ->
     *                                          Status = "Not Published !"
     *                                          Reason = "Service not registered in JSON file !";
     *                                          
     * References: Lab 3
     *             ReadAllLines and WriteAllText : https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalllines?view=net-5.0
     *             Uri.UnescapeDataString and EscapeDataString: https://docs.microsoft.com/en-us/dotnet/api/system.uri.unescapedatastring?view=net-5.0
     *                                                          https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.unescape?view=net-5.0
     *             ASP.NET Web API Tutorial for Beginners _ ASP.NET Web API Crash Course 2020 (720p) : https://www.youtube.com/watch?v=iaeHaydhatE
     *             Creating A REST Webservice With C# And Visual Studio (360p) : https://www.youtube.com/watch?v=LXZnuJY0dGI
     *             Intro to WebAPI - One of the most powerful project types in C# (720p) : https://www.youtube.com/watch?v=vN9NRqv7xmY&t=1186s
     *             C# Tutorial - How to create and Parse JSON Data : https://www.youtube.com/watch?v=NX3Um9E-AY0
     *             Stackover Flow : https://stackoverflow.com/questions/13297563/read-and-parse-a-json-file-in-c-sharp
     *             
     *             
     *             
     */


    public class PublishController : ApiController
    {
        private AuthenticatorServiceLibrary.IAuthenticate authenticateInterface;

        // GET api/publish/{token}?jsonServiceDescriptionString={jsonServiceDescriptionString}
        [Route("api/publish/{token}")]
        [HttpGet]
        public Models.StatusDetailsIntermed publish(String jsonServiceDescriptionString, int token)
        {

            //Object for returning status
            Models.StatusDetailsIntermed statusDetails = new Models.StatusDetailsIntermed();

            //The new service to be added
            Models.ServiceDescriptionIntermed newServiceDescription = new Models.ServiceDescriptionIntermed();

            //Deserializing the inputed json string to add() to serviceList
            newServiceDescription = JsonConvert.DeserializeObject<Models.ServiceDescriptionIntermed>(jsonServiceDescriptionString);

            //Has to be decoded as it is a url
            newServiceDescription.serviceEndPoint = Uri.UnescapeDataString(newServiceDescription.serviceEndPoint);

            //Creating connection via channelfactory in authenticatorAccessInterface
            authenticateInterface = Models.AuthenticatorAccessInterface.remoteConnection();

            string tokenAuthenticationStatus = authenticateInterface.validate(token);

            String jsonString = JsonConvert.SerializeObject(newServiceDescription,Formatting.Indented);

            //If token is valid can proceed with filehandling
            if (tokenAuthenticationStatus.Equals("validated"))
            {


                //Checking if file exists in path and if not have to create a new text file
                if (!File.Exists(SOA_Utilities.Constants.pathForServiceReg))
                {
                    try
                    {
                        //Manually adding the [ and ] as it not will cause an error when deserializing again
                        string newJsonString = $"[{jsonString}]";
                        System.IO.File.WriteAllText(SOA_Utilities.Constants.pathForServiceReg, newJsonString);
                    }
                    catch (NotSupportedException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (DirectoryNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                else
                {
                    /*
                     * If file already exists
                     * Assuming Services are already published
                     * Cannot directly append
                     * 
                     */

                    try
                    {
                        // Reading all the data in the json file
                        var allServiceJsonData = System.IO.File.ReadAllText(SOA_Utilities.Constants.pathForServiceReg);

                        // De-serialize all the objects in the json file
                        var serviceList = JsonConvert.DeserializeObject<List<Models.ServiceDescriptionIntermed>>(allServiceJsonData);

                        // Add the service to the list of services
                        serviceList.Add(newServiceDescription);

                        // Re-serialize to save in json file
                        String JsonFile = JsonConvert.SerializeObject(serviceList, Formatting.Indented);
                        File.WriteAllText(SOA_Utilities.Constants.pathForServiceReg, JsonFile);
                    }
                    catch (ArgumentNullException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (DirectoryNotFoundException e)
                    {
                        Console.WriteLine(e);
                    }
                }


                /*
                 * 
                 * Re-checking if the published service is in the textfile
                 * 
                 */

                statusDetails = Models.Filehandling.jsonFileHandlingStatusCheck(newServiceDescription);

                return statusDetails;


            } else if(tokenAuthenticationStatus.Equals("not validated"))
            {
                statusDetails.Reason = "Authentication Error";
                statusDetails.Status = "Denied";
            }

            return statusDetails;
        }
    }
}