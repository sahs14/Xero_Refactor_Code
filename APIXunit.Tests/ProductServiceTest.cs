using System;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace APIXunit.Tests
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();
        private readonly Mock<ILogger<ProductService>> _loggerMock = new Mock<ILogger<ProductService>>();

        private ProductService _productService;

        public ProductServiceTest()
        {
            _productService = new ProductService(_productRepositoryMock.Object, _loggerMock.Object);
        }

        

        [Fact]
         public async Task ModifyProduct_success()
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

            Assert.Same("Product updated", result.ReturnMessage[0]);
            Assert.True(result.IsSuccess);
            _productRepositoryMock.Verify(x => x.UpdateProduct(Id, productDtoModified), Times.Once());

        }

        [Fact]
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
            Assert.IsType<Product>(result);
            Assert.Equal("Phone", result.Name);
        }

        
        [Fact]
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
            Assert.IsType<Product[]>(result);
        }

    }
}
