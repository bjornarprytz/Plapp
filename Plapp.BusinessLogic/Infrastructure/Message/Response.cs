using MediatR;

namespace Plapp.BusinessLogic
{
    public interface IResponse
    {
        bool Error { get; }
        string Message { get; }
    }
    public static class Response
    {
        public static Response<T> Fail<T>(string message, T data = default) => new Response<T>(data, message, true, false);
        public static Response<Unit> Fail(string message) => new Response<Unit>(Unit.Value, message, true, false);
        public static Response<T> Ok<T>(T data, string message = "") => new Response<T>(data, message, false, false);
        public static Response<Unit> Ok(string message = "") => new Response<Unit>(Unit.Value, message, false, false);
        public static Response<T> Cancel<T>(string message = "") => new Response<T>(default, message, false, true);
        public static Response<Unit> Cancel(string message = "") => new Response<Unit>(Unit.Value, message, false, false);
    }

    public class Response<T> : IResponse
    {
        internal Response(T data, string msg, bool error, bool cancelled)
        {
            Data = data;
            Message = msg;
            Error = error;
            Cancelled = cancelled;
        }

        public T Data { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
        public bool Cancelled { get; set; }
    }
}
