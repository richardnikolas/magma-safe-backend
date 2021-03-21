using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Shared.Exceptions
{
    [Serializable]
    public class RepositoryException : Exception
    {
        public RepositoryExceptionReason Reason { get; }
        public IEnumerable<ErrorMessage> Errors { get; } = new ErrorMessage[] { };

        public RepositoryException() { }
        public RepositoryException(string message) : base(message) { }
        public RepositoryException(
            string message,
            RepositoryExceptionReason reason,
            Exception inner
        ) : base(message, inner)
        {
            Reason = reason;
        }

        public RepositoryException(
            string message,
            RepositoryExceptionReason reason,
            IEnumerable<ErrorMessage> errors
        ) : base(message)
        {
            Errors = errors;
            Reason = reason;
        }
        protected RepositoryException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }

    public enum RepositoryExceptionReason
    {
        BadInput,
        ThirdPartyServiceUnavailability,
        UnexpectedError,
    }
}