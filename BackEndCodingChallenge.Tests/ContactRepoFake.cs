using BackEndCodingChallenge.BO;
using BackEndCodingChallenge.Repo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BackEndCodingChallenge.Tests
{
    public class ContactRepoFake : IContactsRepo
    {
        private List<Contact> contacts;
        public ContactRepoFake()
        {
            var fileData = File.ReadAllText("./data.json");
            contacts = JsonConvert.DeserializeObject<List<Contact>>(fileData);
        }

        public List<Contact> GetAllContacts()
        {
            return contacts;
        }
        public bool CreateContact(ContactDTO contact)
        {
            throw new NotImplementedException();
        }

        public bool DeleteContact(string id)
        {
            throw new NotImplementedException();
        }

  

        public List<Contact> GetAllContactsByCity(string state = null, string city = null)
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(string name)
        {
            throw new NotImplementedException();
        }

        public Contact GetContactByEmail(string email = null, string phoneNumber = null)
        {
            throw new NotImplementedException();
        }

        public bool UpdateContact(string id, ContactDTO contact)
        {
            throw new NotImplementedException();
        }
    }
}
