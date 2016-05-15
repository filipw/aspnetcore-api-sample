using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleApi
{
    public class InMemoryContactRepository : IContactRepository
    {
        private readonly List<Contact> _contacts = new List<Contact>
        {
            new Contact { ContactId = 1, Name = "Filip W", Address = "107 Atlantic Avenue", City = "Toronto", State = "ON", Zip = "M6K 1Y2", Email = "filip.wojcieszyn@climaxmedia.com", Twitter = "filip_woj" },
            new Contact { ContactId = 2, Name = "Josh Donaldson", Address = "1 Blue Jays Way", City = "Toronto", State = "ON", Zip = "M5V 1J1", Email = "joshd@bluejays.com", Twitter = "BringerOfRain20" },
            new Contact { ContactId = 3, Name = "Aaron Sanchez", Address = "1 Blue Jays Way", City = "Toronto", State = "ON", Zip = "M5V 1J1", Email = "aarons@bluejays.com", Twitter = "A_Sanch41" },
            new Contact { ContactId = 4, Name = "Jose Bautista", Address = "1 Blue Jays Way", City = "Toronto", State = "ON", Zip = "M5V 1J1", Email = "joseb@bluejays.com", Twitter = "JoeyBats19" },
            new Contact { ContactId = 5, Name = "Edwin Encarnacion", Address = "1 Blue Jays Way", City = "Toronto", State = "ON", Zip = "M5V 1J1", Email = "edwine@bluejays.com", Twitter = "Encadwin" },
        };

        public IList<Contact> GetAll()
        {
            return _contacts;
        }

        public Contact Get(int id)
        {
            return _contacts.FirstOrDefault(x => x.ContactId == id);
        }

        public void Add(Contact contact)
        {
            if (_contacts.Any(x => x.ContactId == contact.ContactId))
            {
                throw new InvalidOperationException(string.Format("Contact with id '{0}' already exists", contact.ContactId));
            }

            _contacts.Add(contact);
        }

        public void Update(Contact updatedContact)
        {
            var contact = Get(updatedContact.ContactId);
            if (contact == null)
            {
                throw new InvalidOperationException(string.Format("Contact with id '{0}' does not exists", contact.ContactId));
            }

            contact.Address = updatedContact.Address;
            contact.City = updatedContact.City;
            contact.Email = updatedContact.Email;
            contact.Name = updatedContact.Name;
            contact.State = updatedContact.State;
            contact.Twitter = updatedContact.Twitter;
            contact.Zip = updatedContact.Zip;
        }

        public void Delete(int id)
        {
            var contact = Get(id);
            if (contact == null)
            {
                throw new InvalidOperationException(string.Format("Contact with id '{0}' does not exists", contact.ContactId));
            }

            _contacts.Remove(contact);
        }
    }
}