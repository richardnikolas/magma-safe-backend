using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Shared.Exceptions.Database
{
    [Serializable]
    public class UnauthorizedViolationException : BaseException
    {
        public UnauthorizedViolationException(ErrorMessage error)
            : base(error)
        {
        }

        public UnauthorizedViolationException(ErrorMessage error, Exception innerException)
            : base(error, innerException)
        {
        }

        public UnauthorizedViolationException(IEnumerable<ErrorMessage> errors, Exception innerException)
            : base(errors, innerException)
        {
        }

        public UnauthorizedViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
