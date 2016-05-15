using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SampleApi.Filters
{
    public class ContactSelfLinkFilter : ActionFilterAttribute
    {
        private readonly LinkProvider _linkProvider;

        public ContactSelfLinkFilter(LinkProvider linkProvider)
        {
            if (linkProvider == null)
            {
                throw new ArgumentNullException(nameof(linkProvider));
            }
            _linkProvider = linkProvider;
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var response = actionExecutedContext.Result as ObjectResult;

            Contact contact;
            if (response.TryGetContentValue(out contact))
            {
                AddSelfLink(contact, actionExecutedContext.HttpContext);
                return;
            }

            IEnumerable<Contact> contacts;
            if (response.TryGetContentValue(out contacts))
            {
                response.Value = contacts.Select(c => AddSelfLink(c, actionExecutedContext.HttpContext));
            }
        }

        Contact AddSelfLink(Contact contact, HttpContext context)
        {
            contact.Self = _linkProvider.GetLink(context, "GetContactById", new { id = contact.ContactId }).ToString();
            return contact;
        }
    }
}
