using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AuthenticatorServiceLibrary;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace ServiceProvider.Controllers
{
    /*
     * This controller is used for genearting prime numbers in a given range
     * @param for 2 integers with a token to check for validity
     * @returns ResultPrimeNumbers Object which contains the validity of the token and the values in the range in a list
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

    public class GeneratePrimeNumbersInRangeController : ApiController
    {
        private AuthenticatorServiceLibrary.IAuthenticate authenticateInterface;

        // GET api/generateprimes/5/2/tokenvalue
        [Route("api/generateprimes/{number1}/{number2}/{token}")]
        [HttpGet]
        public Models.ResultPrimeNumbers GeneratePrimeNumbersInRange(int number1, int number2, int token)
        {

            string status;
            Models.ResultPrimeNumbers result = new Models.ResultPrimeNumbers();
            List<string> values;

            //create connection with the authenticator
            authenticateInterface = Models.AuthenticatorAccessInterface.remoteConnection();

            status = authenticateInterface.validate(token);

            //token validated
            if (status.Equals("validated"))
            {
                //assigns the list of generated prime numbers in range(number1, number2)
                values = Models.PrimeNumbers.GeneratePrimeNumbers(number1, number2);
                result.Values = values;
                result.Reason = "Validated";
                result.Status = "Returned";
            }
            else if (status.Equals("not validated"))//invalid token
            {
                //not validated hence null Values
                result.Values = null;
                result.Reason = "Authentication Error";
                result.Status = "Denied";
            }

            return result;
        }
    }
}