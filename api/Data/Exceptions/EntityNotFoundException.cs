using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the entity searched by URL params not found.
    /// </summary>
    public sealed class EntityNotFoundException : ApiException
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="T:PetHealth.Core.Exceptions.EntityNotFoundException"></see> class.
        /// </summary>
        public EntityNotFoundException() : base("Entity not found.") { }

        /// <summary>
        /// Initialize a new instance of the <see cref="T:PetHealth.Core.Exceptions.EntityNotFoundException"></see> class with a specified message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public EntityNotFoundException(string message) : base(message) { }
    }
}
