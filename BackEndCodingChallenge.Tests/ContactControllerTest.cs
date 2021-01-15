using BackEndCodingChallenge.API.Controllers;
using BackEndCodingChallenge.Repo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BackEndCodingChallenge.Tests
{
   public class ContactControllerTest
    {

        ContactsController _controller;
        IContactsRepo _repo;

        public ContactControllerTest()
        {
            _repo = new ContactRepoFake();
            _controller = new ContactsController(_repo);
        }

        [Fact]
        public void GetAllContacts_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAllContacts();
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

    }
}
