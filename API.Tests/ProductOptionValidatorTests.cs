using API.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using API.Validators;

namespace API.Tests
{
    [TestClass]
    public class ProductOptionValidatorTests
    {
        [TestMethod]
        public void Validation_Success()
        {
            // Arrange
            var productOption = new ProductOptionDto()
            {
                Name = "Television",
                Description = "41 inch Television"
            };

            //Act
            var errorMessages = productOption.Validate();

            //Assert
            Assert.AreEqual(0, errorMessages.Count);

        }

        [TestMethod]
        public void Validation_Fail_NoName()
        {
            // Arrange
            var productOption = new ProductOptionDto()
            {
                Name = "",
                Description = "41 inch Television"
            };

            //Act
            var errorMessages = productOption.Validate();

            //Assert
            Assert.AreEqual(1, errorMessages.Count);

        }

        [TestMethod]
        public void Validation_Fail_NoDescription()
        {
            // Arrange
            var productOption = new ProductOptionDto()
            {
                Name = "TV",
                Description = ""
            };

            //Act
            var errorMessages = productOption.Validate();

            //Assert
            Assert.AreEqual(1, errorMessages.Count);
        }

        [TestMethod]
        public void Validation_Fail_MutipleError()
        {
            // Arrange
            var productOption = new ProductOptionDto()
            {
                Name = "",
                Description = ""
            };

            //Act
            var errorMessages = productOption.Validate();

            //Assert
            Assert.AreEqual(2, errorMessages.Count);

        }
    }
}