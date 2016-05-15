using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleApi.Controllers
{
    [Produces("text/csv")]
    public class CsvOnlyController : ApiController
    {
        [Route("csv/contact")]
        public IEnumerable<Contact> GetContact()
        {
            var contact = new Contact
            {
                ContactId = 100,
                Address = "Bahnhofstrasse",
                City = "Luzern",
                Email = "filip@strathweb.com",
                Name = "Filip W",
                Zip = "6000",
                State = "LU"
            };

            return new[] { contact };
        }
    }
}