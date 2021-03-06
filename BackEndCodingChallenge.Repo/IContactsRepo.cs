﻿using BackEndCodingChallenge.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndCodingChallenge.Repo
{
    public interface IContactsRepo
    {

        List<Contact> GetAllContacts();

        bool CreateContact(ContactDTO contact);

        Contact GetContact(string name);

        bool UpdateContact(string id, ContactDTO contact);

        bool DeleteContact(string id);

        Contact GetContactByEmail(string email = null, string phoneNumber = null);

        List<Contact> GetAllContactsByCity(string state = null, string city = null);
    }
}
