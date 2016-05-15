using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SampleApi
{
    public class LinkProvider
    {
        public virtual Uri GetLink(HttpContext requestContext, string routeName, object routeParams)
        {
            var urlHelper = requestContext.RequestServices.GetRequiredService<IUrlHelper>();
            return new Uri(urlHelper.Link(routeName, routeParams), UriKind.Absolute);
        }
    }
}