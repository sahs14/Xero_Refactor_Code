using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ProductOptionsServiceTest
    {
         private readonly Mock<IProductOptionRepository> _productOptionRepositoryMock = new Mock<IProductOptionRepository>();
        private readonly Mock<ILogger<ProductOptionService>> _loggerMock = new Mock<ILogger<ProductOptionService>>();

        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();

        private ProductOptionService _productOptionService;

        public ProductOptionsServiceTest()
        {
            _productOptionService = new ProductOptionService(_productOptionRepositoryMock.Object, _productRepositoryMock.Object,_loggerMock.Object);
        }

        [Fact]
        public async Task CreateProduct_success()
        {
            var productDtoOld = new Product()
            {
                Name = "Book",
                Description = "Fiction Books",
                Price = 10,
                DeliveryPrice = 12
            };
            
            var prdDTO = new ProductOptionDto()
            {
                Name = "Television",
                Description = "41 inch Television",
            };
            int productId = 3;

            _productRepositoryMock.Setup(x => x.GetProductByProductOption(productId)).ReturnsAsync(productDtoOld);

            var result = await _productOptionService.CreateProductOption(productId, prdDTO);

            Assert.Same("ProductOption Added", result.ReturnMessage[0]);
            Assert.True(result.IsSuccess);
            _productOptionRepositoryMock.Verify(x => x.CreateProductOption(productId, prdDTO), Times.Once());
        }

        [Fact]
        public async Task GetProductById()
        {

            //Arrange
            int Id=300;

            var prdDTO = new ProductOption()
            {
                Name = "Phone",
                Description = "Mobile Phone list"
            };
            int productId = 1;

            _productOptionRepositoryMock.Setup(x => x.GetProductOptionById(productId, Id)).ReturnsAsync(prdDTO);

            //Act
            var result = await _productOptionService.GetProductOptionById(productId, Id);

            //Assert
            Assert.IsType<ProductOption>(result);
            Assert.Equal("Phone", result.Name);
        }

        
        [Fact]
        public async Task GetProducts()
        {

            ProductOption[] productList = new ProductOption[] {
                new ProductOption()
                {
                    Name = "Phone",
                    Description = "Mobile Phone list"
                },
                new ProductOption()
                {
                    Name = "Book",
                    Description = "Book list"
                }};

            int productId = 1;
            _productOptionRepositoryMock.Setup(x => x.GetProductOptions(productId)).ReturnsAsync(productList);

            //Act
            var result = await _productOptionService.GetProductOptions(productId);

            //Assert
            Assert.IsType<ProductOption[]>(result);
        }
        
    }
}