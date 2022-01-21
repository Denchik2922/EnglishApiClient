﻿using System.Runtime.Serialization;

namespace EnglishApiClient.Infrastructure
{
    [Serializable]
    internal class HttpResponseException : Exception
    {
        public HttpResponseException()
        {
        }
        public HttpResponseException(string message)
            : base(message)
        {
        }
        public HttpResponseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        protected HttpResponseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
