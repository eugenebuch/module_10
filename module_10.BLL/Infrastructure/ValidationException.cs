using System;

namespace module_10.BLL.Infrastructure
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
            // pass
        }
    }
}
