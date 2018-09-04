using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Models
{
    public class ValidationError
    {
        public string Error { get; set; }
        public string Field { get; set; }

        public ValidationError(string error)
        {
            Error = error;
        }

        public ValidationError(string error, string field)
        {
            Error = error;
            Field = field;
        }
    }
}
