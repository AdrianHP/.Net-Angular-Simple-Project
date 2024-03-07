using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    /// <summary>
    /// The exception that is thrown when a login has failed due to wrong credentials.
    /// </summary>
    public class WrongCredentialsException : LoginFailedException
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="T:PetHealth.Core.Exceptions.WrongCredentialsException"></see> class with data for audit.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public WrongCredentialsException(string message) : base(message) { }
    }
}
