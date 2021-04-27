using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace Registry.Models
{
    /*
     * This static class does all the file handling checking operations of the Registry class
     * 
     */
    internal static class Filehandling
    {
        private static StatusDetailsIntermed StatusDetailsIntermed;


        /*
         * jsonFileHandlingStatusCheck() checks if a given service exists in the registry
         * 
         * @param service
         * @returns StatusDetailsIntermed object
         * 
         * 
         * if - > existsInList() -> @returns Status = "Published !" 
         *                         @returns Reason = "Service Registered !" 
         *                         
         * else - >existsInList() -> @returns Status = "Not Published !" 
         *                         @returns Reason = "Service not registered in JSON file !" 
         * References: Lab 3
         *             ReadAllLines and WriteAllText : https://docs.microsoft.com/en-us/dotnet/api/system.io.file.readalllines?view=net-5.0
         *             C# Tutorial - How to create and Parse JSON Data : https://www.youtube.com/watch?v=NX3Um9E-AY0
         *             Stackover Flow : https://stackoverflow.com/questions/13297563/read-and-parse-a-json-file-in-c-sharp
         *             Serialize and de-serialisation : https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0
         */


        public static StatusDetailsIntermed jsonFileHandlingStatusCheck(ServiceDescriptionIntermed service)
        {
            StatusDetailsIntermed = new StatusDetailsIntermed();
      
            // Reading all the data in the json file with the newly added service
            string allServices = System.IO.File.ReadAllText(SOA_Utilities.Constants.pathForServiceReg);

            // De-serialize all the objects in the json file
            var newServiceList = JsonConvert.DeserializeObject<List<Models.ServiceDescriptionIntermed>>(allServices);


            if (existsInlist(newServiceList, service))
            {
                StatusDetailsIntermed.Status = "Published !";
                StatusDetailsIntermed.Reason = "Service Registered !";
            }
            else
            {
                StatusDetailsIntermed.Status = "Not Published !";
                StatusDetailsIntermed.Reason = "Service not registered in JSON file !";
            }

            return StatusDetailsIntermed;
        }


        /*
         * existsInlist checks if a given service exists in an a given list of services
         * 
         * @param List<ServiceDescriptionIntermed> serviceList
         * @param ServiceDescriptionIntermed service
         * @returns StatusDetailsIntermed object
         * 
         */

        public static bool existsInlist(List<ServiceDescriptionIntermed> serviceList,ServiceDescriptionIntermed service)
        {
            //TempObject that fetches if a object if exists in list
            Models.ServiceDescriptionIntermed matches = new Models.ServiceDescriptionIntermed();

            //Check all the names of services in the list for a service.name
            matches = serviceList.Find(x => x.name == service.name);

            if(matches != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}

