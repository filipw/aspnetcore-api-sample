using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace SampleApi
{
    public class LinkProvider
    {
        private IActionContextAccessor _actionContextAccessor;

        public LinkProvider(IActionContextAccessor actionContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
        }

        public virtual Uri GetLink(HttpContext requestContext, string routeName, object routeParams)
        {
            var urlHelperFactory = requestContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            var urlHelper = urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            return new Uri(urlHelper.Link(routeName, routeParams), UriKind.Absolute);
        }
    }
}