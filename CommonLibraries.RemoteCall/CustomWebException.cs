using System;
using System.Net;

namespace CommonLibraries.RemoteCall
{
    public class CustomWebException : WebException
    {
        public string BodyAsString { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public CustomWebException(string message, HttpStatusCode code, string body) : base(message)
        {            
            BodyAsString = body;
            HttpStatusCode = code;
        }

        public CustomWebException(string message, HttpStatusCode code, Exception innerException, string body) : base(message, innerException)
        {
            BodyAsString = body;
            HttpStatusCode = code;
        }
    }
}
