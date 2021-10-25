using API.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using API.Validators;

namespace API.Tests
{
    [TestClass]
    public class ProductValidatorTests
    {
        [TestMethod]
        public void Validation_Success()
        {
            // Arrange
            var product = new ProductDto()
            {
                Name = "Television",
                Description = "41 inch Television",
                Price = 100,
                DeliveryPrice = 110
            };

            //Act
            var errorMessages = product.Validate();

            //Assert
            Assert.AreEqual(0, errorMessages.Count);

        }

        [TestMethod]
        public void Validation_Fail_NoName()
        {
            // Arrange
            var product = new ProductDto()
            {
                Name = "",
                Description = "41 inch Television",
                Price = 100,
                DeliveryPrice = 110
            };

            //Act
            var errorMessages = product.Validate();

            //Assert
            Assert.AreEqual(1, errorMessages.Count);

        }

        [TestMethod]
        public void Validation_Fail_NoDescription()
        {
            // Arrange
            var product = new ProductDto()
            {
                Name = "TV",
                Description = "",
                Price = 100,
                DeliveryPrice = 110
            };

            //Act
            var errorMessages = product.Validate();

            //Assert
            Assert.AreEqual(1, errorMessages.Count);
        }

        [TestMethod]
        public void Validation_Fail_NoPrice()
        {
            // Arrange
            var product = new ProductDto()
            {
                Name = "TV",
                Description = "Testing Description",
                Price = 0,
                DeliveryPrice = 110
            };

            //Act
            var errorMessages = product.Validate();

            //Assert
            Assert.AreEqual(1, errorMessages.Count);

        }

        public void Validation_Fail_NoDeliveryPrice()
        {
            // Arrange
            var product = new ProductDto()
            {
                Name = "TV",
                Description = "TV description",
                Price = 20,
                DeliveryPrice = 0
            };

            //Act
            var errorMessages = product.Validate();

            //Assert
            Assert.AreEqual(1, errorMessages.Count);

        }

        public void Validation_Fail_MutipleError()
        {
            // Arrange
            var product = new ProductDto()
            {
                Name = "TV",
                Description = "",
                Price = 0,
                DeliveryPrice = 0
            };

            //Act
            var errorMessages = product.Validate();

            //Assert
            Assert.AreEqual(3, errorMessages.Count);

        }
    }
}