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
     * This controller is used for adding two numbers
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


    public class AddTwoNumbersController : ApiController
    {
        AuthenticatorServiceLibrary.IAuthenticate authenticateInterface;

        // GET api/addtwo/5/10
        [Route("api/addtwo/{number1}/{number2}/{token}")]
        [HttpGet]
        public Models.Result AddTwoNumbers(int number1, int number2, int token)
        {
            Models.Result result = new Models.Result();
            string status;

            //calling connection and assign to the authenticator interface
            authenticateInterface = Models.AuthenticatorAccessInterface.remoteConnection();

            status = authenticateInterface.validate(token);

            //if token is valid
            if (status.Equals("validated"))
            {
                //has to be in string format as it is the safest native data type for serialization and de-serialization
                string serviceAnswer = (number1 + number2).ToString();
                result.Value = serviceAnswer;
                result.Reason = "Validated";
                result.Status = "Returned";
            }
            //invalid token
            else if (status.Equals("not validated"))
            {
                //valus is not returned as not validated
                result.Value = null;
                result.Reason = "Authentication Error";
                result.Status = "Denied";
            }

            return result;
        }
    }
}