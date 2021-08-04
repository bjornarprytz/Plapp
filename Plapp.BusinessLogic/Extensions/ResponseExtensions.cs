using System;

namespace Plapp.BusinessLogic
{
    public static class ResponseExtensions
    {
        public static IResponseWrapper<TResponse> WrapErrors<TResponse>(this IResponseWrapper response)
        {
            return Response.GenerateTypedErrorResponse<IResponseWrapper<TResponse>>(response.Failures);
        }

        public static void Throw(this IResponseWrapper response)
        {
            throw new Exception();
        }
    }
}