using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace ServicePublisherConsole.Models
{
    /*
     * Model class used as a intermediary for deserialized StatusDetails object retrived from a response
     * 
     * References : JsonProperty :https://www.thecodebuzz.com/json-change-name-of-property-jsonpropertyname-serialize-deserialize/
     */

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class StatusDetailsIntermed
    {
        [JsonProperty(PropertyName = "Status")]
        public String Status { get; set; }

        [JsonProperty(PropertyName = "Reason")]
        public String Reason { get; set; }
    }
}