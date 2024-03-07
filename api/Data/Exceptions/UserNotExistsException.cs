using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the user doesn't exist
    /// </summary>
    public sealed class UserNotExistsException : ObjectNotExistsException
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="T:PetHealth.Core.Exceptions.UserNotExistsException"></see> class.
        /// </summary>
        public UserNotExistsException() : base("User doesn't exist.") { }

        /// <summary>
        /// Initialize a new instance of the <see cref="T:PetHealth.Core.Exceptions.UserNotExistsException"></see> class with a specified message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UserNotExistsException(string message) : base(message) { }
    }
}
