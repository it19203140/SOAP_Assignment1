using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticatorServiceLibrary;
using RestSharp;
using Newtonsoft.Json;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace ServicePublisherConsole.Models
{
    /*
     * This is a static class that is used to create the connection with the Authenticator interface
     * This is used by the ServiceProvider to access the Authenticator
     * 
     *  References : https://docs.microsoft.com/en-us/dotnet/api/system.web.httputility.urlencode?view=net-5.0
     *               https://restsharp.dev/api/RestSharp.Serializers.NewtonsoftJson.html
     *               https://stackoverflow.com/questions/43209924/rest-api-use-the-accept-application-json-http-header
     */

    internal static class RegistryAccess
    {
        private static RestClient restClient;

        //Registry Service Enpoint
        private static string regServiceEndpointURL = "https://localhost:44304/";

        /*
         * @param string of ServiceDetailsIntermed to publish
         * @returns response provided by Registry as IRestResponse
         */
        public static IRestResponse publish(string serviceName, string description, string APIEndpoint, string numberOfOperands, string operandTypes, int token)
        {
            assignURLtoRestClient();

            //Have to decode API endpoint as it is a URL
            String decodedEndPointURL = Uri.EscapeDataString(APIEndpoint);

            //creating and assigning ServiceDetailsIntermed
            ServiceDetailsIntermed serviceDetails = new ServiceDetailsIntermed();
            serviceDetails.name = serviceName;
            serviceDetails.description = description;
            serviceDetails.serviceEndPoint = decodedEndPointURL;
            serviceDetails.numberOfOperands = numberOfOperands;
            serviceDetails.operandType = operandTypes;

            //Serializing object to Json String
            string jsonString = JsonConvert.SerializeObject(serviceDetails);

            // Calling GET api/publish/{token}?jsonServiceDescriptionString={jsonServiceDescriptionString}
            RestRequest request = new RestRequest($"api/publish/{token}?jsonServiceDescriptionString={jsonString}");
            IRestResponse response = restClient.Get(request);

            return response;
        }

        public static void assignURLtoRestClient()
        {
            //Creating an instance of RestClient with the endpoint URL
            restClient = new RestClient(regServiceEndpointURL);
        }

        /*
         * @param APIEndpoint to unpublish
         * @returns response provided by Registry as IRestResponse
         */

        public static IRestResponse unpublish(string APIEndpoint,int token)
        {
            assignURLtoRestClient();

            //Have to decode API endpoint as it is a URL
            String decodedEndPointURL = Uri.EscapeDataString(APIEndpoint);

            ServiceDetailsIntermed serviceDetails = new ServiceDetailsIntermed();

            //assigning endpoint to object
            serviceDetails.serviceEndPoint = decodedEndPointURL;


            //Serialize object to json String
            string jsonString = JsonConvert.SerializeObject(serviceDetails);

            // GET api/unpublish/{token}?serviceEndPointString={serviceEndPointString}
            RestRequest request = new RestRequest($"api/unpublish/{token}?serviceEndPointString={jsonString}");

            IRestResponse response = restClient.Get(request);

            return response;
        }
    }

    /*
     * Model class used as a intermediary for deserialized ServiceDetailsIntermed object retrived from a response
     * 
     */

    //Used for deserialization in run time
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class ServiceDetailsIntermed
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
