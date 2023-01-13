using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace PomeDataLayer.ExtraClasses
{
    [DataContract]
    public class CoreClass
    {

        [DataMember]
        public HttpStatusCode Status;
        [DataMember]
        public string Message;
        [DataMember]
        public string Data;
        [DataMember]
        public string NewRecordID;

        public CoreClass()
        {
            Message = string.Empty;
            Data = string.Empty;
            NewRecordID = string.Empty;
        }
    }
}