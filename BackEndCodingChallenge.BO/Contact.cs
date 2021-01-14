using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace BackEndCodingChallenge.BO
{
   public class Contact
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Name is required")]     
        public string Name { get; set; }
        public string Company { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime Dob { get; set; }
        public Address Address { get; set; }
    }
}
