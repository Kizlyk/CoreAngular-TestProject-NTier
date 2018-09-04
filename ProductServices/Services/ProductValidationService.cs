using ProductDAL;
using ProductDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Utilities.Helpers;
using Utilities.Models;

namespace ProductServices.Services
{
    public class ProductValidationService : IProductValidationService
    {
        private ProductDBContext _productContext;

        public ProductValidationService(ProductDBContext productContext)
        {
            _productContext = productContext;
        }

        public void ValidateProduct(Product product)
        {
            ICollection<ValidationError> errors;
            if (!ValidateProduct(product, out errors))
            {
                var message = ValidationHelper.GetValidationMessage(errors);
                throw new CustomValidationException(message);
            }
        }

        public bool ValidateProduct(Product product, out ICollection<ValidationError> errors)
        {
            errors = new List<ValidationError>();
            if (product == null)
            {
                errors.Add(new ValidationError("No product to validate"));
                return false;
            }

            if (string.IsNullOrEmpty(product.Name))
                errors.Add(new ValidationError("Product name is not specified", "Name"));

            if (string.IsNullOrEmpty(product.Code))
                errors.Add(new ValidationError("Product code is not specified", "Code"));
            else if (!VerifyIfProductCodeIsUnique(product.Code, product.Id))
                errors.Add(new ValidationError("Product code already exists", "Code"));

            if (product.Price < 0)
                errors.Add(new ValidationError("Product price should not be negative", "Price"));

            return errors.Count == 0;
        }

        public bool VerifyIfProductCodeIsUnique(string code, int id)
        {
            return !_productContext.Products.Any(p => p.Code == code && p.Id != id);
        }
    }
}
