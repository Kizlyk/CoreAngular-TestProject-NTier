using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ProductDAL;
using ProductServices.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Microsoft.AspNet.OData.Query;

namespace ProductServices.Services
{
    public class ProductExportService : IProductExportService
    {
        private ProductDBContext _productContext;
        private IProductManagementService _productManagementService;

        public ProductExportService(ProductDBContext productContext, IProductManagementService productManagementService)
        {
            _productContext = productContext;
            _productManagementService = productManagementService;
        }

        public byte[] ExportToExcel(ODataQueryOptions<ProductDTO> options)
        {
            var products = _productManagementService.GetProducts(options);
            return CreateExcelFile(products);
        }

        private byte[] CreateExcelFile(IEnumerable<ProductDTO> products)
        {
            var memory = new MemoryStream();
            IWorkbook workbook;
            workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet("Demo");
            IRow headerRow = excelSheet.CreateRow(0);

            headerRow.CreateCell(0).SetCellValue("Code");
            headerRow.CreateCell(1).SetCellValue("Name");
            headerRow.CreateCell(2).SetCellValue("Price");
            headerRow.CreateCell(3).SetCellValue("Last Updated");

            var rowcount = 1;
            foreach (var product in products)
            {var 
                row = excelSheet.CreateRow(rowcount);
                PopulateProductRow(product, row);
                rowcount++;
            }

            workbook.Write(memory);
            return memory.ToArray();
        }

        private void PopulateProductRow(ProductDTO product, IRow row)
        {
            row.CreateCell(0).SetCellValue(product.Code);
            row.CreateCell(1).SetCellValue(product.Name);
            row.CreateCell(2).SetCellValue(product.Price.ToString());
            row.CreateCell(3).SetCellValue(product.LastUpdated.HasValue ? product.LastUpdated.Value.ToString("g") : "");
        }
    }
}
