using Microsoft.EntityFrameworkCore;
using ProductDAL;
using ProductDAL.Models;
using ProductServices.Services;
using System;
using Utilities.Models;
using Xunit;
using Moq;
using AutoMapper;
using ProductServices.Mappers;
using ProductServices.DTOs;
using System.Linq;

namespace ProductCatalogTests
{
    //unit test examples
    public class UnitTests
    {
        [Fact]
        public void ValidateProductValidationService_NegativePriceProduct()
        {
            var testDbContext = CreateInMemoryDBContext();
            var productValidationService = new ProductValidationService(testDbContext);
            var testProduct = new Product()
            {
                Id = 1,
                Code = "test1",
                Name = "Prod 1",
                Price = -50
            };
            Exception ex = Assert.Throws<CustomValidationException>(() => productValidationService.ValidateProduct(testProduct));
            Assert.Equal("Product price should not be negative", ex.Message);
        }

        [Fact]
        public void ValidateProductManagementService_CreateProduct()
        {
            var testDbContext = CreateInMemoryDBContext();
            var mapper = CreateAutoMapper();
            var testProduct = new ProductDTO()
            {
                Id = 0,
                Code = "test1",
                Name = "Prod 1",
                Price = 120
            };
            var mockValidationService = new Mock<IProductValidationService>();
            var productManagementService = new ProductManagementService(testDbContext, mapper, mockValidationService.Object);
            Assert.Equal(1, productManagementService.CreateProduct(testProduct));
        }

        [Fact]
        public void ValidateProductManagementService_GetAllProducts()
        {
            var testDbContext = CreateInMemoryDBContextWithData();
            var mapper = CreateAutoMapper();
            var mockValidationService = new Mock<IProductValidationService>();
            var productManagementService = new ProductManagementService(testDbContext, mapper, mockValidationService.Object);
            var results = productManagementService.GetProducts(null);
            var resultsCount = results.Count();
            var secondItem = results.ToList()[1];
            Assert.Equal(2, resultsCount);
            Assert.Equal("C2", secondItem.Code);
        }

        private ProductDBContext CreateInMemoryDBContext() {
            var options = new DbContextOptionsBuilder<ProductDBContext>()
                .UseInMemoryDatabase("inmemorydb")
                .Options;
            return new ProductDBContext(options);
        }

        private ProductDBContext CreateInMemoryDBContextWithData()
        {
            var options = new DbContextOptionsBuilder<ProductDBContext>()
                .UseInMemoryDatabase("inmemorydbwithdata")
                .Options;
            var context = new ProductDBContext(options);
            context.Products.Add(new Product { Id = 1, Code = "C1", Name = "First", Photo = "photo1", Price = 10, LastUpdated = DateTime.UtcNow });
            context.Products.Add(new Product { Id = 2, Code = "C2", Name = "Second", Photo = "photo2", Price = 20, LastUpdated = DateTime.UtcNow });
            context.SaveChanges();
            return context;
        }

        private IMapper CreateAutoMapper()
        {
            return new Mapper(
                new MapperConfiguration(
                    configure => { configure.AddProfile<MappingProfile>(); }
                )
            );
        }
    }
}
