using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace ServiceProvider.Controllers
{
    /*
     * This controller is used for checking if number is a prime
     * @param for an integer with a token to check for validity
     * @returns ResultPrimeNumbers Object which contains the validity of the token and the primes in a list
     * 
     * if token "validated" - > Result = List<String>
     *                          Status = "Value returned"
     *                          Reason = "Validated"
     *                          
     * if token "not validated" - > Result = null
     *                          Status = "Denied"
     *                          Reason = "Authentication Error"
     *                          
     * 
     */

    public class isPrimeNumberController : ApiController
    {
        private Models.Result result = new Models.Result();

        private AuthenticatorServiceLibrary.IAuthenticate authenticateInterface;

        // GET api/genprime/5/10
        [Route("api/isprime/{number1}/{token}")]
        [HttpGet]
        public Models.Result isPrime(int number1, int token)
        {
            //create connection with the authenticator
            authenticateInterface = Models.AuthenticatorAccessInterface.remoteConnection();

            string responseStatus = authenticateInterface.validate(token);

            if (responseStatus.Equals("validated"))
            {
                //assign generated primes for a given value
                bool isPrime = Models.PrimeNumbers.isPrime(number1);
                if (isPrime)
                {
                    result.Value = $"True {number1} is prime";
                }
                else
                {
                    result.Value = $"False {number1} is not a prime";
                }
                result.Reason = "Validated";
                result.Status = "Value Returned";
            }
            else if (responseStatus.Equals("not validated"))//invalid token
            {
                //assign null as will not be providing the service as the user was not authenticated
                //will not be sending the result as not validated
                result.Value = null;
                result.Reason = "Authentication Error";
                result.Status = "Denied";

            }

            return result;
        }
    }
}