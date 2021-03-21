using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MagmaSafe.Shared.Models;

namespace MagmaSafe.Shared.Exceptions
{
    [Serializable]
    public class BadRequestException : BaseException
    {
        public BadRequestException(IEnumerable<ErrorMessage> errors) : base(errors) { }

        protected BadRequestException(
           SerializationInfo info,
           StreamingContext context) : base(info, context) { }
    }
}
