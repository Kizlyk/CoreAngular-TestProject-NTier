using Microsoft.AspNet.OData.Query;
using ProductServices.DTOs;

namespace ProductServices.Services
{
    public interface IProductExportService
    {
        byte[] ExportToExcel(ODataQueryOptions<ProductDTO> options);
    }
}