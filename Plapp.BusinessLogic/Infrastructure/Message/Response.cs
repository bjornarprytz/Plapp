using System.Collections;
using MediatR;

namespace Plapp.BusinessLogic
{
    public class Response
    {
        public string Message { get; set; }
        public bool Error { get; set; }
        public bool Cancelled { get; set; }

        protected Response(string message, bool error,  bool cancelled)
        {
            Cancelled = cancelled;
            Error = error;
            Message = message;
        }
        
        public static Response<T> Fail<T>(string message, T data = default) => new Response<T>(data, message, true, false);
        public static Response Fail(string message) => new Response(message, true, false);
        public static Response<T> Ok<T>(T data, string message = "") => new Response<T>(data, message, false, false);
        public static Response<Unit> Ok(string message = "") => new Response<Unit>(Unit.Value, message, false, false);
        public static Response<T> Cancel<T>(string message = "") => new Response<T>(default, message, false, true);
        public static Response Cancel(string message = "") => new Response(message, false, false);
    }

    public class Response<T> : Response
    {
        internal Response(T data, string msg, bool error, bool cancelled) : base(msg, error, cancelled)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
