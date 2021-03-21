using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Shared.Exceptions.Database
{
    [Serializable]
    public class ForeignKeyViolationException : BaseException
    {
        public ForeignKeyViolationException(ErrorMessage error)
            : base(error)
        {
        }

        public ForeignKeyViolationException(ErrorMessage error, Exception innerException)
            : base(error, innerException)
        {
        }

        public ForeignKeyViolationException(IEnumerable<ErrorMessage> errors, Exception innerException)
            : base(errors, innerException)
        {
        }

        public ForeignKeyViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
