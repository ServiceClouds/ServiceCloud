using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Response
{
    public sealed record Error
    {
        public static readonly Error None = new(string.Empty);

        public static readonly Error NullValue =
            new("Null value was provided.");

        public Error(string description)
        {
            Description = description;
        }

        public string Description { get; }

        public static Error Validation(string description)
            => new(description);

        public static Error NotFound(string description)
            => new(description);

        public static Error Conflict(string description)
            => new(description);

        public static Error Unauthorized(string description)
            => new(description);

        public static Error Forbidden(string description)
            => new(description);

        public static Error Failure(string description)
            => new(description);
    }
}
