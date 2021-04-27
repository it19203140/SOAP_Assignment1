using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace Registry.Controllers
{
        /*
         * 
         * Controller class that unpublish service details
         * @param is in JSON format with a token integer (ServiceDescription json string and a token)
         * @returns in Models.StatusDetailsIntermed with contains the status of the Unpublish service
         * if token not validated -> @returns -> 
         *                                          Reason = "Authentication Error";
                                                    Status = "Denied";

         * token validated && service published -> @returns ->
         *                                          Status = "Published !";
                                                    Reason = "Service Registered !";

         * token validated but service not published -> @returns ->
         *                                          Status = "Not Published !"
         *                                          Reason = "Service not registered in JSON file !";
         * References: Lab 3
         *             ReadAllLines and WriteAllText : https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalllines?view=net-5.0
         *             Uri.UnescapeDataString and EscapeDataString: https://docs.microsoft.com/en-us/dotnet/api/system.uri.unescapedatastring?view=net-5.0
         *                                                          https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.unescape?view=net-5.0
         *             ASP.NET Web API Tutorial for Beginners _ ASP.NET Web API Crash Course 2020 (720p) : https://www.youtube.com/watch?v=iaeHaydhatE
         *             Creating A REST Webservice With C# And Visual Studio (360p) : https://www.youtube.com/watch?v=LXZnuJY0dGI
         *             Intro to WebAPI - One of the most powerful project types in C# (720p) : https://www.youtube.com/watch?v=vN9NRqv7xmY&t=1186s
         *             C# Tutorial - How to create and Parse JSON Data : https://www.youtube.com/watch?v=NX3Um9E-AY0
         *             Search query : https://docs.microsoft.com/en-us/powerapps/developer/data-platform/org-service/linq-query-examples
         */

    public class UnpublishController : ApiController
    {
        //authenicationInterface to access authentication services
        private AuthenticatorServiceLibrary.IAuthenticate authenticateInterface;

        //Status object o return status;
        private Models.StatusDetailsIntermed statusIntermed = new Models.StatusDetailsIntermed();

        // GET api/unpublish/{token}?serviceEndPointString={serviceEndPointString}]
        [Route("api/unpublish/{token}")]
        [HttpGet]
        public Models.StatusDetailsIntermed unpublish(String serviceEndPointString, int token)
        {
            string status;
            List<Models.ServiceDescriptionIntermed> allServicesList = new List<Models.ServiceDescriptionIntermed>();
            Models.ServiceDescriptionIntermed serviceToRemove = new Models.ServiceDescriptionIntermed();

            //creating a remote connection
            authenticateInterface = Models.AuthenticatorAccessInterface.remoteConnection();

            //getting return if token is validated
            status = authenticateInterface.validate(token);

            if (status.Equals("validated"))
            {

                //Fetch all json services to jsonString
                String getAllServices = File.ReadAllText(SOA_Utilities.Constants.pathForServiceReg);

                //De-serialize all services to List<Models.ServiceDescriptionIntermed>
                allServicesList = JsonConvert.DeserializeObject<List<Models.ServiceDescriptionIntermed>>(getAllServices);

                //De-serialize to object form
                var serviceEndpoint = JsonConvert.DeserializeObject<Models.ServiceDescriptionIntermed>(serviceEndPointString);

                //Finding the service with the respective endpoint in the list
                serviceToRemove = allServicesList.Find(x => x.serviceEndPoint == serviceEndpoint.serviceEndPoint);

                //service not found
                if(serviceToRemove == null)
                {
                    statusIntermed.Status = "Unsuccessful!";
                    statusIntermed.Reason = "Service not published to unpublish or error";

                }//service exists in list
                else
                {

                    //Unpublishing service
                    allServicesList.Remove(serviceToRemove);

                    //De-serialize and have in serviceDetails.txt

                    String serviceListJsonFile = JsonConvert.SerializeObject(allServicesList, Formatting.Indented);

                    File.WriteAllText(SOA_Utilities.Constants.pathForServiceReg, serviceListJsonFile);

                    statusIntermed.Status = "Successful!";
                    statusIntermed.Reason = $"Unpublished from file path {SOA_Utilities.Constants.pathForServiceReg}";
                }

                return statusIntermed;
            }

            else if(status.Equals("not validated"))
            {
                statusIntermed.Reason = "Authentication Error";
                statusIntermed.Status = "Denied";
            }
            else
            {
                statusIntermed.Reason = "Logic Error";
                statusIntermed.Status = "Terminated";
            }

            return statusIntermed;
        }
    }
}