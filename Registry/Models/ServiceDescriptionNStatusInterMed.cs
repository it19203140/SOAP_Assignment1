using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using Newtonsoft.Json;

//SLIIT ID : IT19203140
//Curtin ID : 20520193
//Name with initials : K.S. Jayasekera
//Contact number : 0768058696

namespace Registry.Models
{
    /*
     * This is a model class to set/get details of a List<ServiceDescriptionIntermed> with reason and status of the endpoint invocation
     * This is mainly used as an intermediary for -> serialization -> .NET object to JSON string
     *                                            -> de-serialization -> JSON string to .NET object
     * References: Lab 3
     *             Serialization and de-serialiaztion : https://www.youtube.com/watch?v=4MsgcLfDVwg
     *                                                  https://stackoverflow.com/questions/25145609/how-to-return-a-json-object-from-a-c-sharp-method
     *                                                  https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-5.0
     *             JsonProperty :https://www.thecodebuzz.com/json-change-name-of-property-jsonpropertyname-serialize-deserialize/
     *             
     */

    //This is used to tell the deserializer to the members with the JsonProperty arribute shoulds be serialized
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class ServiceDescriptionNStatusInterMed
    {
            //JsonProperty helps to serialiser to serialize with the specified name
            [JsonProperty(PropertyName = "Services")]
            public List<ServiceDescriptionIntermed> Services { set; get; }

            [JsonProperty(PropertyName = "Reason")]
            public string Reason { set; get; }

            [JsonProperty(PropertyName = "Status")]
            public string Status { set; get; }
    }
}