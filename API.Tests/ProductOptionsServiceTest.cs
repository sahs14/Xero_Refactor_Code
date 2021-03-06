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
    public class ProductOptionsServiceTest
    {
        private readonly Mock<IProductOptionRepository> _productOptionRepositoryMock = new Mock<IProductOptionRepository>();
        private readonly Mock<ILogger<ProductOptionService>> _loggerMock = new Mock<ILogger<ProductOptionService>>();

        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();

        private ProductOptionService _productOptionService;

        [TestInitialize]
        public void Initialise()
        {
            _productOptionService = new ProductOptionService(_productOptionRepositoryMock.Object, _productRepositoryMock.Object,_loggerMock.Object);
        }

        [TestMethod]
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

            Assert.AreEqual("ProductOption Added", result.ReturnMessage[0]);
            Assert.IsTrue(result.IsSuccess);
            _productOptionRepositoryMock.Verify(x => x.CreateProductOption(productId, prdDTO), Times.Once());
        }

        [TestMethod]
        public async Task UpdateProduct()
        {
            //Arrange
            int Id = 100;

            var productDtoOld = new ProductOptionDto()
            {
                Name = "Book",
                Description = "Fiction Books",
            };


            var productDtoModified = new ProductOptionDto()
                {
                    Name = "Book",
                    Description = "All Books",
                };
            int productId = 1;

            _productOptionRepositoryMock.Setup(x => x.GetProductOptionById(productId, Id)).ReturnsAsync((ProductOption)null);

            _productOptionRepositoryMock.Setup(x => x.UpdateProductOption(Id, productId, productDtoModified)).Verifiable();

            //Act
            await _productOptionService.UpdateProductOption(Id, productId, productDtoModified);

            _productOptionRepositoryMock.Verify(x => x.UpdateProductOption(Id, productId, productDtoModified), Times.Once());

        }

        [TestMethod]
        public async Task DeleteProduct()
        {
            
            //Arrange
            int Id = 200;

            var prdDTO = new ProductOptionDto()
            {
                Name = "Phone",
                Description = "Mobile Phone list"
            };
             int productId = 1;

            _productOptionRepositoryMock.Setup(x => x.GetProductOptionById(productId, Id)).ReturnsAsync((ProductOption)null);

            _productOptionRepositoryMock.Setup(x => x.DeleteProductOption(Id)).Verifiable();

            //Act
            await _productOptionService.DeleteProductOption(Id);

            _productOptionRepositoryMock.Verify(x => x.DeleteProductOption(Id), Times.Once());
        }

        [TestMethod]
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
            Assert.IsInstanceOfType(result, typeof(ProductOption));
            Assert.AreEqual("Phone", result.Name);
        }

        
        [TestMethod]
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
            Assert.IsInstanceOfType(result, typeof(ProductOption[]));
        }
    }
}
