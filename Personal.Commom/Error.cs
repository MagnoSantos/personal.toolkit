using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;

namespace Personal.Commom
{
    public class Error
    {
        private Error(InnerError innerError)
        {
            Errors = ImmutableList.Create(innerError);
        }

        public IEnumerable<InnerError> Errors { get; set; }

        public static Error FromDefault(Exception ex) =>
            new(InnerError.FromDefault(ex, HttpStatusCode.InternalServerError));
    }
}