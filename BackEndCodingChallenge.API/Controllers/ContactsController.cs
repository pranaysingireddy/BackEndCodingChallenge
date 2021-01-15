using BackEndCodingChallenge.BO;
using BackEndCodingChallenge.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndCodingChallenge.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IContactsRepo _repo;

        public ContactsController(IContactsRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Returns all contacts
        /// </summary>
        /// <returns>return all contacts</returns>
        [HttpGet]
        [Route("GetAllContacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllContacts()
        {

            return Ok(_repo.GetAllContacts());
        }

        /// <summary>
        /// Return the Contact by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>return a single contct</returns>
        [HttpGet]
        [Route("GetContact/{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetContact(string userName)
        {
            var contact = _repo.GetContact(userName);

            if (contact != null)
            {
                return Ok(contact);
            }
            return NoContent();


        }

        /// <summary>
        /// Save a contact
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="file"></param>
        /// <returns>return HttpStatusCode of result</returns>
        [HttpPost]
        [Route("createcontact")]
        public IActionResult CreateContact([ModelBinder(BinderType = typeof(JsonModelBinder))] ContactDTO contact,IFormFile file)
        {
            contact.Id = Guid.NewGuid().ToString();

            if (file != null&& file.Length > 0)
            {
                contact.Image = file;
            }

            _repo.CreateContact(contact);

            return Ok();
        }


        /// <summary>
        ///  Update a contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contact"></param>
        /// <returns>return HttpStatusCode of result</returns>
        [HttpPut]
        [Route("updateContact/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateContact(string id,[ModelBinder(BinderType = typeof(JsonModelBinder))] ContactDTO contact, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file!=null &&file.Length > 0 && Path.GetExtension(file.FileName) == "JPEG")
                {
                    contact.Image = file;
                }

                if (_repo.UpdateContact(id, contact))
                {
                    return Ok();
                }
                else
                {
                    ModelState.AddModelError("Update Contact", "Error Occured");
                }

            }
            return BadRequest(ModelState);

        }


        /// <summary>
        /// Delete a contact by Id
        /// </summary>        /// 
        /// <param name="id"></param>
        /// <returns>return HttpStatusCode</returns>
        [HttpPost]
        [Route("deletecontact/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteContact(string id)
        {
            if (ModelState.IsValid)
            {
                if (_repo.DeleteContact(id))
                {
                    return Ok();
                }
                else
                {
                    ModelState.AddModelError("Delete Contact", "Error Occured");
                }

            }
            return BadRequest(ModelState);

        }

        /// <summary>
        /// Get a contact with phone number or email
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <returns>a Contact</returns>
        [HttpGet]
        [Route("GetContact")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetContactByPhone(string phoneNumber = null, string email = null)
        {

            Contact contact = _repo.GetContactByEmail(phoneNumber, email);
            if (contact != null)
            {
                return Ok(contact);
            }
            else
            {
                return NoContent();
            }

        }

        /// <summary>
        /// Get all contacts with same city or state
        /// </summary>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <returns>List of contacts</returns>
        [HttpGet]
        [Route("GetContacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetContactsByCity(string city = null, string state = null)
        {

            List<Contact> contacts = _repo.GetAllContactsByCity(city, state);
            if (contacts.Any())
            {
                return Ok(contacts);
            }
            else
            {
                return NoContent();
            }

        }
    }
}
