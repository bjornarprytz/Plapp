
namespace Plapp.BusinessLogic
{
    public static class ResponseExtensions
    {
        public static Response<T> Nest<T>(this Response inner)
        {
            return Response.Fail<T>(inner.Message);
        }

        public static void Throw(this Response response)
        { 
            // TODO: Add proper error handling in the view. Then remove this function

            throw new System.Exception(response.Message);
        }
    }
}
