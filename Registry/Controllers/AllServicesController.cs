using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.IO;
using AuthenticatorServiceLibrary;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace Registry.Controllers
{
    public class AllServicesController : ApiController
    {
        private AuthenticatorServiceLibrary.IAuthenticate authenticateInterface;

        /*
         * 
         * This function returns all services saved in  a jsonfile with a respective status details
         * which will be consumed by the client GUI
         * @returns List<Models.ServiceDescriptionNStatusInterMed>
         * @param token is used to check if user is validated
         * 
         * References: Lab 3
         *             ReadAllLines : https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalllines?view=net-5.0
         * 
         */

        // GET api/getAllServices/{token}
        [Route("api/getAllServices/{token}")]
        [HttpGet]
        public Models.ServiceDescriptionNStatusInterMed allServices(int token)
        {
            string status;
            Models.ServiceDescriptionNStatusInterMed serviceDescription = new Models.ServiceDescriptionNStatusInterMed();

            //creating a connection with the Authenticator via static AuthenticatorAccessInterface.remoteConnection()
            authenticateInterface = Models.AuthenticatorAccessInterface.remoteConnection();

            //fetch result provided by the authenticator
            status = authenticateInterface.validate(token);

            //If token funtion return result is "validated" will return all services
            if (status.Equals("validated"))
            {
                List<Models.ServiceDescriptionIntermed> services = new List<Models.ServiceDescriptionIntermed>();

                //Read all text in appropriate json text file
                String allServicesJson = File.ReadAllText(SOA_Utilities.Constants.pathForServiceReg);

                //Deserialize all services in json string to List<Models.ServiceDescriptionIntermed> object
                services = JsonConvert.DeserializeObject<List<Models.ServiceDescriptionIntermed>>(allServicesJson);

                //assigning to object of return type
                serviceDescription.Services = services;
                serviceDescription.Status = "Access Guranteed!";
                serviceDescription.Reason = "Validated";
                return serviceDescription;
            }

            //If token funtion return result is not validated return authentication error
            else if (status.Equals("not validated"))//invalid token
            {
                serviceDescription.Reason = "Authentication Error";
                serviceDescription.Status = "Denied";

                return serviceDescription;
            }

            //If token funtion return result is not either "not validated" or "validated" means there is a internal code error
            //could be a business logic error in the in the authenticator class
            else
            {
                serviceDescription.Reason = "Logic Error";
                serviceDescription.Reason = "Authentication return error !";

                return serviceDescription;
            }

        }
    }
}