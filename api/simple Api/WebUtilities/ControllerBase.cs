using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Server.WebUtilities
{
    /// <summary>
    /// Represents the class of base controller
    /// </summary>
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// Returns the URL consisting of the current domain and the specified tail.
        /// </summary>
        /// <param name="tail">The tail which should be added to the end of the URL.</param>
        protected string CalculateUrl(string tail)
            => Request.Scheme + "://" + Request.Host + "/" + tail;

        protected async Task<IActionResult> NoContent(Func<Task> action)
        {
            await action();
            return NoContent();
        }

        protected IActionResult NoContent(Action action)
        {
            action();
            return NoContent();
        }

        protected CreatedResult Created<TEntityDTO, TKey>(TEntityDTO result)
            where TEntityDTO : IDTO<TKey>
            where TKey : IEquatable<TKey>
        {
            var idStringValue = Convert.ToString(result.Id);
            return Created($"{Request.Scheme}://{Request.Host.Value}{Request.Path}/{idStringValue}", result);
        }
    }
}
