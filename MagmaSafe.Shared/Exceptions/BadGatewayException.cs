using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Shared.Exceptions
{
    [Serializable]
    public class BadGatewayException : Exception
    {
        public ErrorData Error { get; private set; }
        public BadGatewayException() { }

        public BadGatewayException(string errorData) : base(errorData)
        {
            Error = new ErrorData { Body = errorData };
        }

        public BadGatewayException(string statusCode, string errorData) : base(string.Join(statusCode, errorData))
        {
            Error = new ErrorData { StatusCode = statusCode, Body = errorData };
        }

        protected BadGatewayException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        public class ErrorData
        {
            public string StatusCode { get; set; }
            public string Body { get; set; }
        }
    }
}
