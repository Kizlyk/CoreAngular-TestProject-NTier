using System.Collections.Generic;
using Microsoft.AspNet.OData.Query;
using ProductServices.DTOs;

namespace ProductServices.Services
{
    public interface IProductManagementService
    {
        int CreateProduct(ProductDTO product);
        void DeleteProduct(int productId);
        ProductDTO GetProductByCode(string code);
        ProductDTO GetProductById(int id);
        IEnumerable<ProductDTO> GetProducts(ODataQueryOptions<ProductDTO> options);
        void UpdateProduct(ProductDTO product);
        void InsertTestData();
    }
}