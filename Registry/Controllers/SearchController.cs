using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.IO;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace Registry.Controllers
{
    public class SearchController : ApiController
    {
        private AuthenticatorServiceLibrary.IAuthenticate authenticateInterface;
        private Models.StatusDetailsIntermed statusIntermed = new Models.StatusDetailsIntermed();

        // GET api/search/{"add"}/{token}
        [Route("api/search/{query}/{token}")]
        [HttpGet]

        /*
         * 
         * This function searches for services in the registry
         * @param query which is the query
         * @returns Models.ServiceDescriptionNStatusInterMed which contains a list of services and the status of the function invocation
         * 
         * References: Lab 3
         *             ReadAllLines and WriteAllText : https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalllines?view=net-5.0
         *             Uri.UnescapeDataString and EscapeDataString: https://docs.microsoft.com/en-us/dotnet/api/system.uri.unescapedatastring?view=net-5.0
         *                                                          https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.unescape?view=net-5.0
         *             ASP.NET Web API Tutorial for Beginners _ ASP.NET Web API Crash Course 2020 (720p) : https://www.youtube.com/watch?v=iaeHaydhatE
         *             Creating A REST Webservice With C# And Visual Studio (360p) : https://www.youtube.com/watch?v=LXZnuJY0dGI
         *             Intro to WebAPI - One of the most powerful project types in C# (720p) : https://www.youtube.com/watch?v=vN9NRqv7xmY&t=1186s
         *             C# Tutorial - How to create and Parse JSON Data : https://www.youtube.com/watch?v=NX3Um9E-AY0
         *             Stackover flow : https://stackoverflow.com/questions/9854917/how-can-i-find-a-specific-element-in-a-listt
         *             Search query : https://docs.microsoft.com/en-us/powerapps/developer/data-platform/org-service/linq-query-examples
         */
        public Models.ServiceDescriptionNStatusInterMed search(String query, int token)
        {
            string tokenStatus;
            
            //create connection with the authenticator
            authenticateInterface = Models.AuthenticatorAccessInterface.remoteConnection();

            //fetch token status
            tokenStatus = authenticateInterface.validate(token);

            Models.ServiceDescriptionNStatusInterMed descriptionNStatusInterMed = new Models.ServiceDescriptionNStatusInterMed();

            List<Models.ServiceDescriptionIntermed> serviceDescriptionsList = new List<Models.ServiceDescriptionIntermed>();
            Models.ServiceDescriptionIntermed service = new Models.ServiceDescriptionIntermed();


            //checking if token is "validated"
            if (tokenStatus.Equals("validated"))
            {

                //Checking if file exists
                if (File.Exists(SOA_Utilities.Constants.pathForServiceReg))
                {
                    //Read all text and deserialise
                    string allServicesJSONFormat = File.ReadAllText(SOA_Utilities.Constants.pathForServiceReg);
                    serviceDescriptionsList = JsonConvert.DeserializeObject<List<Models.ServiceDescriptionIntermed>>(allServicesJSONFormat);

                    //Search query in the deserialized list and fetch it in an IEnumerable list
                    var fetch =
                        from serviceDescription in serviceDescriptionsList
                            //where servicedescrition name = query
                    where serviceDescription.name.Contains(query)
                        select serviceDescription;

                    //To list converts to a list format, assign to the model class list(this can only be done as the list is IEnumerable)
                    serviceDescriptionsList = fetch.ToList();

                    //has to be assigned to ServiceDescriptionNStatusInterMed as the return type needs both the status and the list
                    descriptionNStatusInterMed.Services = serviceDescriptionsList;

                    return descriptionNStatusInterMed;
                }
                else //There are no services as no json file so return no services status
                {

                    descriptionNStatusInterMed.Status = "No services!";
                    descriptionNStatusInterMed.Reason = "No JSON file!";

                    return descriptionNStatusInterMed;
                }

            }

            //if token not validated
            else if (tokenStatus.Equals("not validated"))

            {
                descriptionNStatusInterMed.Reason = "Authentication Error";
                descriptionNStatusInterMed.Status = "Denied";

                return descriptionNStatusInterMed;

            }

            //login error in the authenticator class
            else

            {
                descriptionNStatusInterMed.Reason = "Logic Error";
                descriptionNStatusInterMed.Status = "Terminated";

                return descriptionNStatusInterMed;
            }
        }
    }
}