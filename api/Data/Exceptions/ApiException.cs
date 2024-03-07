using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    /// <summary>
    /// Represents the base class for API layer. Use this inside controllers.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PetHealth.Core.Exceptions.ApiException"></see> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ApiException(string message) : base(message) { }
    }
}
