using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Shared.Response
{
    public sealed class Result<T> : Result
    {
        private readonly T? _value;

        internal Result(T? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        [NotNull]
        public T Value =>
            IsSuccess
                ? _value!
                : throw new InvalidOperationException(
                    "Cannot access the value of a failed result.");

        public static Result<T> Success(T value)
            => new(value, true, Error.None);

        public static Result<T> Failure(Error error)
            => new(default, false, error);

        public static implicit operator Result<T>(T value)
            => Success(value);
    }
}
