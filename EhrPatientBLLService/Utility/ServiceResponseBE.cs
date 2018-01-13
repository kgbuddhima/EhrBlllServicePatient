using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Utility
{
    public class ServiceResponseBE
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string ResponseMessage { get; set; }
        public string Error { get; set; }
    }
}
