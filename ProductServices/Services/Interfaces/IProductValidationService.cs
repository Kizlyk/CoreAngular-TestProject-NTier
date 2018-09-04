using System.Collections.Generic;
using ProductDAL.Models;
using Utilities.Models;

namespace ProductServices.Services
{
    public interface IProductValidationService
    {
        void ValidateProduct(Product product);
        bool ValidateProduct(Product product, out ICollection<ValidationError> errors);
        bool VerifyIfProductCodeIsUnique(string code, int id);
    }
}