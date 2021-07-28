
namespace Plapp.BusinessLogic
{
    public static class ResponseExtensions
    {
        public static Response<T> Nest<T>(this IResponse inner)
        {
            return Response.Fail<T>(inner.Message);
        }

        public static void Throw(this IResponse response)
        { 
            // TODO: Remove this once I've added proper error handling in the view.

            throw new System.Exception(response.Message);
        }
    }
}
