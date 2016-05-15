using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi
{
    public static class ObjectResultExtensions
    {
        public static bool TryGetContentValue<T>(this ObjectResult objectResult, out T value) where T : class
        {
            value = default(T);
            var responseMessage = objectResult.Value as HttpResponseMessage;
            var content = responseMessage?.Content as ObjectContent;
            if (content?.Value is T)
            {
                value = (T)content.Value;
                return true;
            }

            var responseObject = objectResult.Value as T;
            if (responseObject != null)
            {
                value = responseObject;
                return true;
            }

            return false;
        }
    }
}