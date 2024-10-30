using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class WebhookDto
    {
         public DateTime Time { get; set; }
        public string SrcUser { get; set; }
        public string FullyQualifiedSubjectUserName { get; set; }
        public string ClientName { get; set; }
        public string ClientIPAddress { get; set; }
        public string NASIdentifier { get; set; }
        public string NASPort { get; set; }
        public string CallingStationID { get; set; }
        public string ProxyPolicyName { get; set; }
        public string AuthenticationServer { get; set; }
        public string AuthenticationType { get; set; }
    }
}