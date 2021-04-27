using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
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
     * This is class that is used to create the connection with the Service Provider
     * This is used by the WPF_Client to access the Service Provider Services
     * 
     * 
     */

    public class ServiceProviderAccessObject
    {
        private RestClient restClient = new RestClient();

        /*
         * This function fetches any reponse invoked by the client -> Addtion or Multipcation or The generate prime numbers function
         * 
         * The ServiceProviderIntermed returns are of two types -> string (invoke of the addition or multipication services will return a single string)
         *                                                      -> List<string> (invocation of generation of prime number will have many values)
         *                                                      
         *        To handle this a try catch block has been implemented in the concatMessage() static function.
         * 
         * 
         * @param APIEndPoint
         * @returns ServiceProviderIntermed
         * 
         *      References : Lab 3
                Serialise and de-serialize : https://stackoverflow.com/questions/13297563/read-and-parse-a-json-file-in-c-sharp
                                             RestSharp
         * 
         */


        public ServiceProviderIntermed response(string APIEndPoint)
        {
            ServiceProviderIntermed intermed = new ServiceProviderIntermed();
            RestRequest restRequest = new RestRequest(APIEndPoint);

            //Invoke get request with the given endpoint
            IRestResponse response = restClient.Get(restRequest);

            //De-serialize json return
            intermed = JsonConvert.DeserializeObject<ServiceProviderIntermed>(response.Content);

            return intermed;
        }

        /*
         * This function will concat all the values and status information to be dispayed on a MessageBox to the user
         * @param string message, ServiceProvider intermed object to concat
         * 
         * In this function it is intially assumed that the function will not generate a System.ArgumentNullException
         * If generated will be handled in the catch block where it is assumed that the Exception was caused as there is a single return (multiply, substraction function)
         * 
         * @return concat string
         * 
         */
        public static string concatMessage(string message, ServiceProviderIntermed intermed)
        {
            try
            {
                //intermed.Values will be null if invoke a single string return service
                //It may which will generate a System.ArgumentNullException
                if (intermed.Values.Count() > 0)
                {
                    message = "Values : ";
                    //foreach value concat to string
                    foreach (String value in intermed.Values)
                    {
                        message = message + $"{value}, ";
                    }

                    if (!String.IsNullOrWhiteSpace(intermed.Status))
                    {
                        message = message + "\n";
                        message = message + "Status : ";
                        message = message + $"{intermed.Status}";
                    }

                    if (!String.IsNullOrWhiteSpace(intermed.Reason))
                    {
                        message = message + "\n";
                        message = message + "Reason : ";
                        message = message + $"{intermed.Reason}";
                    }
                }
            }
            catch (System.ArgumentNullException)
            {
                //System.ArgumentNullException means that it is a single string , reason and status returned

                if (!String.IsNullOrWhiteSpace(intermed.Value))
                {
                    message = $"value is : {intermed.Value} \n";
                    if (!String.IsNullOrWhiteSpace(intermed.Status))
                    {
                        message = message + "\n";
                        message = message + "Status : ";
                        message = message + $"{intermed.Status}";
                    }

                    if (!String.IsNullOrWhiteSpace(intermed.Reason))
                    {
                        message = message + "\n";
                        message = message + "Reason : ";
                        message = message + $"{intermed.Reason}";
                    }
                }
                else //this means the token has not been validated and so have a "Status" and "Reason"
                {
                    if (!String.IsNullOrWhiteSpace(intermed.Status))
                    {
                        message = message + "Status : ";
                        message = message + $"{intermed.Status}";
                    }

                    if (!String.IsNullOrWhiteSpace(intermed.Reason))
                    {
                        message = message + "\n";
                        message = message + "Reason : ";
                        message = message + $"{intermed.Reason}";
                    }
                }
            }

            return message;
        }

    }

    /*
     * 
     * Model class for de-serialization
     * 
     */

    public class ServiceProviderIntermed
    {
        public List<string> Values { set; get; }

        public string Value { set; get; }

        public string Status { set; get; }

        public string Reason { set; get; }
    }
}
