using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    public class BusinessException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }
}
