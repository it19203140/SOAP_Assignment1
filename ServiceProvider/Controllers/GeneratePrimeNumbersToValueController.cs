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
     * This controller is used for genearting prime numbers for a given number
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
    public class GeneratePrimeNumbersToValueController : ApiController
    {
        private AuthenticatorServiceLibrary.IAuthenticate authenticateInterface;

        // GET api/genprime/5/10
        [Route("api/genprime/{number1}/{token}")]
        [HttpGet]
        public Models.ResultPrimeNumbers generatePrimeNumbers(int number1,int token)
        {
            string status;
            Models.ResultPrimeNumbers result = new Models.ResultPrimeNumbers();
            List<string> values;

            //create channel with channelFactory
            authenticateInterface = Models.AuthenticatorAccessInterface.remoteConnection();

            status = authenticateInterface.validate(token);

            //token validated
            if (status.Equals("validated"))
            {
                //assign generated primes for a given value
                values = Models.PrimeNumbers.GeneratePrimeNumbers(number1);
                result.Values = values;
                result.Reason = "Validated";
                result.Status = "Returned";
            }
            else if (status.Equals("not validated"))//invalid token
            {
                //assign null as will not be providing the service as the user was not authenticated
                result.Values = null;
                result.Reason = "Authentication Error";
                result.Status = "Denied";
            }

            return result;
        }
    }
}