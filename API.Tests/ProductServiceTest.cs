using Microsoft.VisualStudio.TestTools.UnitTesting;
using API;
using Moq;
using API.Interfaces;
using API.Services;
using API.Entities;
using API.DTOs;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace API.Tests
{
    [TestClass]
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();
        private readonly Mock<ILogger<ProductService>> _loggerMock = new Mock<ILogger<ProductService>>();
        private ProductService _productService;

        [TestInitialize]
        public void Initialise()
        {
            _productService = new ProductService(_productRepositoryMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task CreateProduct()
        {
            var prdDTO = new ProductDto()
            {
                Name = "Television",
                Description = "41 inch Television",
                Price = 100,
                DeliveryPrice = 110
            };

            await _productService.CreateProduct(prdDTO);

            _productRepositoryMock.Verify(x => x.CreateProduct(prdDTO), Times.Once());
        }

        [TestMethod]
        public async Task UpdateProduct()
        {
            //Arrange
            int Id = 100;

            var productDtoOld = new ProductDto()
            {
                Name = "Book",
                Description = "Fiction Books",
                Price = 10,
                DeliveryPrice = 12
            };


            var productDtoModified = new ProductDto()
                {
                    Name = "Book",
                    Description = "All Books",
                    Price = 50,
                    DeliveryPrice = 60
                };


            _productRepositoryMock.Setup(x => x.GetProductById(Id)).ReturnsAsync((Product)null);

            _productRepositoryMock.Setup(x => x.UpdateProduct(Id, productDtoModified)).Verifiable();

            //Act
            await _productService.UpdateProduct(Id,productDtoModified);

            _productRepositoryMock.Verify(x => x.UpdateProduct(Id, productDtoModified), Times.Once());

        }

         [TestMethod]
        public async Task ModifyProduct_sucess()
        {
            //Arrange
            int Id = 100;

            var productDtoOld = new ProductDto()
            {
                Name = "Book",
                Description = "Fiction Books",
                Price = 10,
                DeliveryPrice = 12
            };


            var productDtoModified = new ProductDto()
                {
                    Name = "Book",
                    Description = "All Books",
                    Price = 50,
                    DeliveryPrice = 60
                };


            _productRepositoryMock.Setup(x => x.GetProductById(Id)).ReturnsAsync((Product)null);

            _productRepositoryMock.Setup(x => x.UpdateProduct(Id, productDtoModified)).Verifiable();

            //Act
            var result = await _productService.ModifyProduct(Id,productDtoModified);

            Assert.AreEqual("Product updated", result.ReturnMessage[0]);
            Assert.IsTrue(result.IsSuccess);
            _productRepositoryMock.Verify(x => x.UpdateProduct(Id, productDtoModified), Times.Once());

        }

        [TestMethod]
        public async Task ModifyProduct_fail()
        {
            //Arrange
            int Id = 0;

            var productDtoOld = new ProductDto()
            {
                Name = "Book",
                Description = "Fiction Books",
                Price = 10,
                DeliveryPrice = 12
            };


            var productDtoModified = new ProductDto()
                {
                    Name = "Book",
                    Description = "All Books",
                    Price = 50,
                    DeliveryPrice = 60
                };


            _productRepositoryMock.Setup(x => x.GetProductById(Id)).ReturnsAsync((Product)null);

            _productRepositoryMock.Setup(x => x.UpdateProduct(Id, productDtoModified)).Verifiable();

            //Act
            var result = await _productService.ModifyProduct(Id,productDtoModified);

            Assert.AreEqual("Product Id invalid", result.ReturnMessage[0]);
            Assert.IsFalse(result.IsSuccess);
            _productRepositoryMock.Verify(x => x.UpdateProduct(Id, productDtoModified), Times.Never());

        }

        [TestMethod]
        public async Task DeleteProduct()
        {
            
            //Arrange
            int Id = 200;

            var prdDTO = new ProductDto()
            {
                Name = "Phone",
                Description = "Mobile Phone list",
                Price = 100,
                DeliveryPrice = 110
            };

            _productRepositoryMock.Setup(x => x.GetProductById(Id)).ReturnsAsync((Product)null);

            _productRepositoryMock.Setup(x => x.DeleteProduct(Id)).Verifiable();

            //Act
            await _productService.DeleteProduct(Id);

            _productRepositoryMock.Verify(x => x.DeleteProduct(Id), Times.Once());
        }

        [TestMethod]
        public async Task GetProductById()
        {

            //Arrange
            int Id=300;

            var prdDTO = new Product()
            {
                Name = "Phone",
                Description = "Mobile Phone list",
                Price = 100,
                DeliveryPrice = 110
            };

            _productRepositoryMock.Setup(x => x.GetProductById(Id)).ReturnsAsync(prdDTO);

            //Act
            var result = await _productService.GetProductById(Id);

            //Assert
            Assert.IsInstanceOfType(result, typeof(Product));
            Assert.AreEqual("Phone", result.Name);
        }

        
        [TestMethod]
        public async Task GetProducts()
        {

            Product[] productList = new Product[] {
                new Product()
                {
                    Name = "Phone",
                    Description = "Mobile Phone list",
                    Price = 100,
                    DeliveryPrice = 110
                },
                new Product()
                {
                    Name = "Book",
                    Description = "Book list",
                    Price = 120,
                    DeliveryPrice = 130
                }};

            _productRepositoryMock.Setup(x => x.GetProducts()).ReturnsAsync(productList);

            //Act
            var result = await _productService.GetProducts();

            //Assert
            Assert.IsInstanceOfType(result, typeof(Product[]));
        }
    }
}
