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
     * This controller is used for muliplying two numbers
     * @param for two numbers with a token to check for validity
     * @returns Result Object which contains the validity of the token and the result
     * 
     * if token "validated" - > Result = $"{answer}"
     *                          Status = "Value returned"
     *                          Reason = "Validated"
     *                          
     * if token "not validated" - > Result = null
     *                          Status = "Denied"
     *                          Reason = "Authentication Error"
     *                          
     * 
     */

    public class MulTwoNumbersController : ApiController
    {
        private AuthenticatorServiceLibrary.IAuthenticate authenticateInterface;

        // GET api/multiplytwo/5/10
        [Route("api/multiplytwo/{number1}/{number2}/{token}")]
        [HttpGet]
        public Models.Result MulTwoNumbers(int number1, int number2,int token)
        {
            Models.Result result = new Models.Result();
            string status;

            //connecting to the autheticator 
            authenticateInterface = Models.AuthenticatorAccessInterface.remoteConnection();

            status = authenticateInterface.validate(token);

            if (status.Equals("validated"))
            {
                string serviceAnswer = (number1 * number2).ToString();
                result.Value = serviceAnswer;
                result.Reason = "Validated";
                result.Status = "Returned";
            }
            else if (status.Equals("not validated"))//invalid token
            {
                result.Value = null;
                result.Reason = "Authentication Error";
                result.Status = "Denied";
            }

            return result;
        }
    }
}