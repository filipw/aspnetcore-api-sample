using System.Collections.Generic;

namespace SampleApi
{
    public interface IContactRepository
    {
        IList<Contact> GetAll();

        Contact Get(int id);

        void Add(Contact contact);

        void Update(Contact updatedContact);

        void Delete(int id);
    }
}