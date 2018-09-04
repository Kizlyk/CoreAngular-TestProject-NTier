using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using ProductDAL;
using ProductDAL.Models;
using ProductServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Utilities.Models;

namespace ProductServices.Services
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IMapper _mapper;
        private ProductDBContext _productContext;
        private IProductValidationService _productValidationService;

        public ProductManagementService(ProductDBContext productContext, IMapper mapper, IProductValidationService productValidationService)
        {
            _productContext = productContext;
            _mapper = mapper;
            _productValidationService = productValidationService;
        }

        public IEnumerable<ProductDTO> GetProducts(ODataQueryOptions<ProductDTO> options)
        {
            var products = _productContext.Products.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider);
            if (options != null)
            {
                products = options.ApplyTo(products) as IQueryable<ProductDTO>;
            }
            return products.ToList();
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _mapper.Map<Product, ProductDTO>(_productContext.Products.FirstOrDefault(p => p.Id == id));
            if (product == null)
            {
                throw new CustomNotFoundException("Product not found");
            }
            return product;
        }

        public ProductDTO GetProductByCode(string code)
        {
            var product = _mapper.Map<Product, ProductDTO>(_productContext.Products.FirstOrDefault(p => p.Code == code));
            if (product == null)
            {
                throw new CustomNotFoundException("Product not found");
            }
            return product;
        }

        public int CreateProduct(ProductDTO product)
        {
            var dbProduct = _mapper.Map<ProductDTO, Product>(product);
            _productValidationService.ValidateProduct(dbProduct);
            dbProduct.LastUpdated = DateTime.UtcNow;
            _productContext.Products.Add(dbProduct);
            _productContext.SaveChanges();
            return dbProduct.Id;
        }

        public void UpdateProduct(ProductDTO product)
        {
            var dbProduct = product != null ? _productContext.Products.FirstOrDefault(p => p.Id == product.Id) : null;
            if (dbProduct == null)
            {
                throw new CustomNotFoundException("Product not found");
            }
            _mapper.Map<ProductDTO, Product>(product, dbProduct);
            _productValidationService.ValidateProduct(dbProduct);
            dbProduct.LastUpdated = DateTime.UtcNow;
            _productContext.Entry(dbProduct).OriginalValues["Timestamp"] = product.Timestamp;
            try
            {
                _productContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new CustomException("Cannot update this product, it has already changed");
            }
        }

        public void DeleteProduct(int productId)
        {
            var product = _productContext.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                throw new CustomNotFoundException("Product not found");
            }
            _productContext.Products.Remove(product);
            _productContext.SaveChanges();
        }

        //for testing only
        public void InsertTestData()
        {
            if (!_productContext.Products.Any())
            {
                _productContext.Products.Add(new Product { Code = "1", Name = "First Product", Price = 100, Photo = "https://cdn2.gsmarena.com/vv/pics/apple/apple-iphone-5s-ofic.jpg", LastUpdated = DateTime.UtcNow });
                _productContext.Products.Add(new Product { Code = "2", Name = "Second Product", Price = 200, Photo = "https://cdn2.gsmarena.com/vv/pics/apple/apple-iphone-6-1.jpg", LastUpdated = DateTime.UtcNow });
                _productContext.Products.Add(new Product { Code = "3", Name = "Third Product", Price = 300, Photo = null, LastUpdated = DateTime.UtcNow });
                _productContext.SaveChanges();
            }
        }
    }
}
