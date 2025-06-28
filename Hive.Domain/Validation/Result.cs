using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Domain.Validation
{
    public class Result<T>
    {
        public T? Value { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public IReadOnlyCollection<string> Errors { get; }

        protected internal Result(bool isSuccess, T? value, IEnumerable<string> errors)
        {
            Value = value;
            IsSuccess = isSuccess;
            Errors = errors.ToArray();
        }

        public static Result<T> Success(T value)
            => new Result<T>(true, value, System.Array.Empty<string>());

        public new static Result<T> Failure(string error)
            => new Result<T>(false, default, new[] { error });

        public new static Result<T> Failure(IEnumerable<string> errors)
            => new Result<T>(false, default, errors);
    }
}
