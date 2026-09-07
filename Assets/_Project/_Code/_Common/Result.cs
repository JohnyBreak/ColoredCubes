namespace _Project._Code._Common
{
    public struct Result<T>
    {
        public readonly T Object;
        public readonly bool IsSuccess;

        public Result(T result, bool success)
        {
            Object = result;
            IsSuccess = success;
        }

        public static Result<T> Success(T result)
        {
            return new Result<T>(result, true);
        }

        public static Result<T> Fail()
        {
            return new Result<T>(default, false);
        }
    }
}