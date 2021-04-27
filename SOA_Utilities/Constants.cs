using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace SOA_Utilities
{
    /*
     * Contains constants which will be useful for the project
     * Includes file paths 
     */
    public static class Constants
    {
        //The path for registration text file
        public static string pathForRegistrationText = @"C:\readandwrite\reg.txt";

        //The path for token save text file
        public static string pathForTokenText = @"C:\readandwrite\token.txt";

        //The path for service registry text file
        public static string pathForServiceReg = @"C:\readandwrite\ServiceDetails.txt";

        // Constant for header login 
        public static int TOKEN = 2;
        public static int REG = 1;
    }
}
