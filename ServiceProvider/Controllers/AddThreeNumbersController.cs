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
     * This controller is used for adding three numbers
     * @param for three numbers with a token to check for validity
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


    public class AddThreeNumbersController : ApiController
    {
        private AuthenticatorServiceLibrary.IAuthenticate authenticateInterface;

        // GET api/addthree/5/2/3/tokenvalue
        [Route("api/addthree/{number1}/{number2}/{number3}/{token}")]
        [HttpGet]
        public Models.Result AddThreeNumbers(int number1, int number2, int number3,int token)
        {
            Models.Result result = new Models.Result();
            string status;

            //creating connection with the Authenticator to check if token is valid
            authenticateInterface = Models.AuthenticatorAccessInterface.remoteConnection();

            //returns validity status 
            status = authenticateInterface.validate(token);
            
            //token validated
            if(status.Equals("validated"))
            {
                //need to convert to string as string is the safest native data type for serialization 
                string serviceAnswer = (number1 + number2 + number3).ToString();
                result.Value = serviceAnswer;
                result.Reason = "Validated";
                result.Status = "Value Returned";
            }
            else if(status.Equals("not validated"))//invalid token
            {
                //will not be sending the result as not validated
                result.Value = null;
                result.Reason = "Authentication Error";
                result.Status = "Denied";
            }

            return result;
        }
    }
}