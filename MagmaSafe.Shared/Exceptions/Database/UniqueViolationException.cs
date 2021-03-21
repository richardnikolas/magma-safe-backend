using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Shared.Exceptions.Database
{
    [Serializable]
    public class UniqueViolationException : BaseException
    {
        public UniqueViolationException(ErrorMessage error)
            : base(error)
        {
        }

        public UniqueViolationException(ErrorMessage error, Exception innerException)
            : base(error, innerException)
        {
        }

        public UniqueViolationException(IEnumerable<ErrorMessage> errors, Exception innerException)
            : base(errors, innerException)
        {
        }

        public UniqueViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
