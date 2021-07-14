using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plapp.BusinessLogic
{
    public static class Response
    {
        public static Response<T> Fail<T>(string message, T data = default) => new Response<T>(data, message, true);
        public static Response<Unit> Fail(string message) => new Response<Unit>(Unit.Value, message, false);
        public static Response<T> Ok<T>(T data, string message = "") => new Response<T>(data, message, false);
        public static Response<Unit> Ok(string message = "") => new Response<Unit>(Unit.Value, message, false);
    }

    public class Response<T>
    {
        internal Response(T data, string msg, bool error)
        {
            Data = data;
            Message = msg;
            Error = error;
        }

        public T Data { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
    }
}
