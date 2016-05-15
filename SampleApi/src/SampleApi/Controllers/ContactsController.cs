using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Filters;

namespace SampleApi.Controllers
{
    [ServiceFilter(typeof(ContactSelfLinkFilter))]
    [FormatFilter]
    [Route("[controller]")]
    public class ContactsController : ApiController
    {
        private readonly IContactRepository _repository;

        public ContactsController(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IEnumerable<Contact> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet("{id}", Name = "GetContactById")]
        public HttpResponseMessage Get(int id)
        {
            var contact = _repository.Get(id);
            if (contact == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]Contact contact)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(contact);
                return CreatedAtRoute("GetContactById", new { id = contact.ContactId }, contact);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public HttpResponseMessage Put(int id, [FromBody]Contact contact)
        {
            contact.ContactId = id;
            _repository.Update(contact);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var deleted = _repository.Get(id);
            if (deleted == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            _repository.Delete(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}