using BackEndCodingChallenge.BO;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.FileProviders;

namespace BackEndCodingChallenge.Repo
{
    public class ContactsRepo : IContactsRepo
    {

        private static List<Contact> contacts = new List<Contact>();
        public List<string> errors;


        //Reading the data.json and assigning it to a static list so that the data will be persisten thorugh out the appplication for testing.
        //In real time data will be accessed by querying actual Db.
        public ContactsRepo()
        {
            errors = new List<string>();
            if (contacts.Count == 0)
            {
                var fileData = File.ReadAllText("./data.json");
                contacts = JsonConvert.DeserializeObject<List<Contact>>(fileData);
            }
        }
        public List<Contact> GetAllContacts()
        {
            return contacts;
        }

        public bool CreateContact(Contact contact)
        {
            try
            {
                contact.Id = System.Guid.NewGuid().ToString();
                contacts.Add(contact);
                //if(contact.Image.Length > 0){
                //    SaveContactImage(contact);
                //}
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateContact1(ContactDTO contactDto)
        {
            try
            {
                if (contactDto.Image.Length > 0)
                {
                    var file = contactDto.Image;
                    var fileNameExtension = Path.GetExtension(contactDto.Image.FileName);
                    contactDto.ImageUrl = String.Concat(contactDto.Id, fileNameExtension);

                    SaveContactImage(contactDto);
                }

                Contact cont = new Contact
                {
                    Id = contactDto.Id,
                    Name = contactDto.Name,
                    Company = contactDto.Company,
                    Dob = contactDto.Dob,
                    ImageUrl=contactDto.ImageUrl,
                    Address = contactDto.Address != null ? contactDto.Address : new Address(),
                };

                contacts.Add(cont);
               

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Contact GetContact(string name)
        {
            return contacts.Where(a => a.Name == name).FirstOrDefault();
        }

        public List<Contact> GetAllContactsByCity( string city = null,string state = null)
        {
            return contacts.Where(a => a.Address?.City == city || a.Address?.State == state).ToList();
        }

        public Contact GetContactByEmail(string phoneNumber = null, string email = null)
        {
            return contacts.Where(a => a.Email == email || (a.PhoneNumber?.PersonalPhone == phoneNumber || a.PhoneNumber?.WorkPhone == phoneNumber)).FirstOrDefault();
        }

       

       

        public bool UpdateContact(string id, Contact contact)
        {
            var record = contacts.Where(a => a.Id == id).FirstOrDefault();
            if (record != null)
            {
                record.Name = contact.Name;
                record.Email = contact.Name;
                record.Company = contact.Company;
                record.Dob = contact.Dob;

                if (contact.Address != null)
                {
                    record.Address.Address1 = contact.Address.Address1;
                    record.Address.Address2 = contact.Address.Address2;
                    record.Address.City = contact.Address.City;
                    record.Address.State = contact.Address.State;
                    record.Address.ZipCode = contact.Address.ZipCode;
                    record.Address.Country = contact.Address.Country;
                }

                if (contact.PhoneNumber != null)
                {
                    if (record.PhoneNumber != null)
                    {
                        record.PhoneNumber.PersonalPhone = contact.PhoneNumber.PersonalPhone;
                        record.PhoneNumber.WorkPhone = contact.PhoneNumber.WorkPhone;
                    }
                    else
                    {
                        PhoneNumber number = new PhoneNumber
                        {
                            PersonalPhone = contact.PhoneNumber.PersonalPhone,
                            WorkPhone = contact.PhoneNumber.WorkPhone
                        };

                        record.PhoneNumber = number;

                    }
                }
                else {
                    record.PhoneNumber = null;
                }

                return true;
            }
            return false;
        }

        public bool DeleteContact(string id)
        {
            var contactToRemove = contacts.Where(a => a.Id == id).FirstOrDefault();
            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);
                return true;
            }
            return false;
        }


        private void SaveContactImage(ContactDTO contact) {

            var file = contact.Image;

            // Combines two strings into a path.
            var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{contact.ImageUrl}";

            using (FileStream fs = System.IO.File.Create(filepath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

    }
}
