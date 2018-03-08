using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.CommonUtilities
{
    public class Result<T> : Result
    {
        public Result(T value, bool isSuccessed, string errorMessage) : base(isSuccessed, errorMessage)
        {
            this.Value = value;
        }

        public Result()
        {

        }

        public T Value { get; set; } = default(T);

    }

    public class Result
    {

        public bool IsSuccessed { get; set; } = false;

        public List<string> Errors { get; set; } = new List<string>();

        public Result()
        {

        }

        public Result(bool isSuccessed, string errorMessage)
        {
            this.IsSuccessed = isSuccessed;
            this.Errors.Add(errorMessage);
        }

        public void AddErrorMessage(string errorMessage)
        {
            this.Errors.Add(errorMessage);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result Fail(string errorMessage)
        {
            return new Result(false, errorMessage);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result<T> Fail<T>(string errorMessage)
        {
            return new Result<T>(default(T), false, errorMessage);
        }

        public string GetErrorString()
        {
            return string.Join(Environment.NewLine, this.Errors.Select(x => x).ToArray());
        }
    }

    public static class ResultExtensions
    {
        public static Task<Result> ToTask(this Result result)
        {
            var taskSource = new TaskCompletionSource<Result>();
            taskSource.SetResult(result);
            return taskSource.Task;
        }

        public static Task<Result<T>> ToTask<T>(this Result<T> result)
        {
            var taskSource = new TaskCompletionSource<Result<T>>();
            taskSource.SetResult(result);
            return taskSource.Task;
        }
    }
}
