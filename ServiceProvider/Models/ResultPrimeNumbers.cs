using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace ServiceProvider.Models
{
    /*
     * This is a model class to set/get List of prime numbers and the token validation details
     * This is mainly used as an intermediary for -> serialization -> .NET object to JSON string
     *                                            -> de-serialization -> JSON string to .NET object
     * 
     */

    public class ResultPrimeNumbers
    {
        public List<string> Values { set; get; }
        public string Status { set; get; }
        public string Reason { set; get; }
    }
}