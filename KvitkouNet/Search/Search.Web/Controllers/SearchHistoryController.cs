using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Search.Data.Models;
using Search.Logic.Services;

namespace Search.Web.Controllers
{
    /// <summary>
    /// Controller for search history.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/history")]
    [ApiController]
    public class SearchHistoryController : ControllerBase
    {
        private readonly ISearchHistoryService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchHistoryController"/> class.
        /// </summary>
        /// <param name="service">The search history service.</param>
        public SearchHistoryController(ISearchHistoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets the last ticket search for user.
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, typeof(TicketSearchEntity), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(object), Description = "The last search for this user doesn't exist")]
        [HttpGet, Route("tickets")]
        public async Task<IActionResult> GetLastTicketSearch()
        {
            var result = await _service.GetLastTicketSearchAsync(GetUserId()).ConfigureAwait(false);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        /// <summary>
        /// Gets the last user search for user.
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserSearchEntity), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(object), Description = "The last search for this user doesn't exist")]
        [HttpGet, Route("users")]
        public async Task<IActionResult> GetLastUserSearch()
        {
            var result = await _service.GetLastUserSearchAsync(GetUserId()).ConfigureAwait(false);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        private string GetUserId()
        {
            if (Request.Headers.ContainsKey("Authorization"))
            {
                var handler = new JwtSecurityTokenHandler();
                var header = Request.Headers["Authorization"].First().Substring(7);
                var token = handler.ReadToken(header) as JwtSecurityToken;
                return token?.Claims.First(claim => claim.Type == "sub").Value;
            }

            return null;
        }
    }
}