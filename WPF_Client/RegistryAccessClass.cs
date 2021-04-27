using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace WPF_Client
{

    /*
     * 
     * 
     * This is used by the WPF_Client to access the Registry
     * 
     * References : Lab 3
     *              Serialise and de-serialize : https://stackoverflow.com/questions/13297563/read-and-parse-a-json-file-in-c-sharp
     *                                           RestSharp
     */


    internal class RegistryAccessClass
    {
        private static RestClient restClient;

        //API end point of the Registry
        private static string regServiceEndpointURL = "https://localhost:44304/";

        //ServiceDescriptionNStatusInterMed comprises of ServiceDescription and StatusDetails intermed 
        private static ServiceDescriptionNStatusInterMed descriptionNStatusInterMed = new ServiceDescriptionNStatusInterMed();


        /*
         * 
         * 
         * @param int token provided
         * @returns ServiceDescriptionNStatusInterMed
         * 
         * 
         * 
         */


        public static ServiceDescriptionNStatusInterMed getAllServices(int token)
        {
            assignURLtoRestClient();

            descriptionNStatusInterMed = null;

            //Service endpoint to access getAllServices function
            RestRequest request = new RestRequest($"api/getAllServices/{token}");

            //Fetching GET method type response
            IRestResponse response = restClient.Get(request);

            //De-serialize from JSON result to .NET object type
            descriptionNStatusInterMed = JsonConvert.DeserializeObject<ServiceDescriptionNStatusInterMed>(response.Content);

            return descriptionNStatusInterMed;
        }


        /*
         * 
         * Assigning URL to RestClient for RestSharp Package
         * 
         */
        private static void assignURLtoRestClient()
        {
            restClient = new RestClient(regServiceEndpointURL);
        }

        /*
         * 
         * @param query for a given service, token for validation by the authenticator
         * @returns ServiceDescriptionNStatusInterMed with the services and status of the service invocation(check token by validator)
         */

        public static ServiceDescriptionNStatusInterMed searchServices(string query, int token)
        {
            assignURLtoRestClient();

            descriptionNStatusInterMed = null;

            //Service endpoint to access search function
            RestRequest restRequest = new RestRequest($"api/search/{query}/{token}");

            //GET request
            IRestResponse response = restClient.Get(restRequest);

            //De-serialize
            descriptionNStatusInterMed = JsonConvert.DeserializeObject<ServiceDescriptionNStatusInterMed>(response.Content);
            return descriptionNStatusInterMed;
        }


    }

    /*
     *
     *
     *Contains all the model classes for de-serialization
     *
     *
     * 
     */


    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ServiceDescriptionNStatusInterMed
    {
        [JsonProperty(PropertyName = "Services")]
        public List<ServiceDescriptionIntermed> Services { set; get; }

        [JsonProperty(PropertyName = "Reason")]
        public string Reason { set; get; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { set; get; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ServiceDescriptionIntermed
    {
        [JsonProperty(PropertyName = "name")]
        public string name { set; get; }

        [JsonProperty(PropertyName = "description")]
        public string description { set; get; }

        [JsonProperty(PropertyName = "serviceEndPoint")]
        public string serviceEndPoint { set; get; }

        [JsonProperty(PropertyName = "numberOfOperands")]
        public string numberOfOperands { set; get; }

        [JsonProperty(PropertyName = "operandType")]
        public string operandType { set; get; }
    }
}
