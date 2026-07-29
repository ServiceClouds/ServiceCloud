using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Response
{
    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new ArgumentException("A successful result cannot contain an error.");

            if (!isSuccess && error == Error.None)
                throw new ArgumentException("A failed result must contain an error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public static Result Success()
            => new(true, Error.None);

        public static Result Failure(Error error)
            => new(false, error);
    }
}
