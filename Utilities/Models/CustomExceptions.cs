using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Models
{
    public class CustomException : Exception
    {
        public virtual int HttpStatusCode
        {
            get
            {
                return (int)System.Net.HttpStatusCode.InternalServerError;
            }
        }

        public CustomException(string userMessage)
            : base(userMessage)
        {
        }

        public CustomException(string userMessage, Exception innerException)
            : base(userMessage, innerException)
        {
        }
    }

    public class CustomNotFoundException : CustomException
    {
        public override int HttpStatusCode
        {
            get
            {
                return (int)System.Net.HttpStatusCode.NotFound;
            }
        }

        public CustomNotFoundException(string userMessage)
            : base(userMessage)
        {
        }

        public CustomNotFoundException(string userMessage, Exception innerException)
            : base(userMessage, innerException)
        {
        }
    }

    public class CustomValidationException : CustomException
    {
        public override int HttpStatusCode
        {
            get
            {
                return (int)System.Net.HttpStatusCode.ExpectationFailed;
            }
        }

        public CustomValidationException(string userMessage)
            : base(userMessage)
        {
        }

        public CustomValidationException(string userMessage, Exception innerException)
            : base(userMessage, innerException)
        {
        }
    }

    public class CustomSessionExpiredException : CustomException
    {
        public override int HttpStatusCode
        {
            get
            {
                return (int)System.Net.HttpStatusCode.Unauthorized;
            }
        }

        public CustomSessionExpiredException(string userMessage)
            : base(userMessage)
        {
        }

        public CustomSessionExpiredException(string userMessage, Exception innerException)
            : base(userMessage, innerException)
        {
        }
    }

    public class CustomForbiddenException : CustomException
    {
        public override int HttpStatusCode
        {
            get
            {
                return (int)System.Net.HttpStatusCode.Forbidden;
            }
        }

        public CustomForbiddenException(string userMessage)
            : base(userMessage)
        {
        }

        public CustomForbiddenException(string userMessage, Exception innerException)
            : base(userMessage, innerException)
        {
        }
    }
}
