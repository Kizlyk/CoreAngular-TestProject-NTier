using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Helpers;
using ProductServices.DTOs;
using ProductServices.Services;
using Utilities.Models;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [CustomExceptionFilter]
    public class ProductController : ODataController
    {
        private IProductManagementService _productManagementService;
        private IProductExportService _productExportService;

        public ProductController(IProductManagementService productManagementService, IProductExportService productExportService)
        {
            _productManagementService = productManagementService;
            _productExportService = productExportService;
        }

        [HttpGet("api/products")]
        public IEnumerable<ProductDTO> GetProducts(ODataQueryOptions<ProductDTO> options)
        {
            return _productManagementService.GetProducts(options);
        }

        [HttpGet("api/product/{id:int}")]
        public ProductDTO GetProduct(int id)
        {
            return _productManagementService.GetProductById(id);
        }

        [HttpPost("api/product")]
        public int CreateProduct([FromBody] ProductDTO product)
        {
            return _productManagementService.CreateProduct(product);
        }

        [HttpPut("api/product")]
        public void UpdateProduct([FromBody] ProductDTO product)
        {
            _productManagementService.UpdateProduct(product);
        }

        [HttpDelete("api/product/{id:int}")]
        public void DeleteProduct(int id)
        {
            _productManagementService.DeleteProduct(id);
        }

        [HttpGet("api/products/exportexcel")]
        public FileResult ExportToExcel(ODataQueryOptions<ProductDTO> options)
        {
            var excel = _productExportService.ExportToExcel(options);
            return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "products.xlsx");
        }
    }
}