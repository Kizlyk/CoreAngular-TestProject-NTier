using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Models;
using System.Linq;

namespace Utilities.Helpers
{
    public class ValidationHelper
    {
        public static string GetValidationMessage(IEnumerable<ValidationError> errors)
        {
            var combinedMessage = new StringBuilder();
            foreach(var error in errors.Where(e => e != null && !string.IsNullOrEmpty(e.Error)))
            {
                if (combinedMessage.Length > 0)
                {
                    combinedMessage.Append("; "); 
                }
                combinedMessage.Append(error.Error);
            }
            return combinedMessage.ToString();
        }
    }
}
