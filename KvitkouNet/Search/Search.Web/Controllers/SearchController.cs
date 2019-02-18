using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EasyNetQ;
using KvitkouNet.Messages.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using Search.Logic.Common.Fakers;
using Search.Logic.Common.Models;
using Search.Logic.Services;

namespace Search.Web.Controllers
{
    /// <summary>
    /// Controller for search domain.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/search")]
    public class SearchController : Controller
    {
        private readonly ISearchUserService _userService;
        private readonly ISearchTicketService _ticketService;
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController" /> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="ticketService">The ticket service.</param>
        public SearchController(ISearchUserService userService, ISearchTicketService ticketService, IBus bus)
        {
            _userService = userService;
            _ticketService = ticketService;
            _bus = bus;
        }

        /// <summary>
        /// Searches the tickets.
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, typeof(SearchResult<TicketInfo>), Description = "All OK")]
        [HttpGet, Route("tickets")]
        public async Task<IActionResult> SearchTickets(TicketSearchRequest request)
        {
            await PublishSearchQueryLogMessage(request);
            SearchResult<TicketInfo> result = await _ticketService.Search(request, GetUserId());
            return Ok(result);
        }

        /// <summary>
        /// Searches the users.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK, typeof(SearchResult<UserInfo>), Description = "All OK")]
        [HttpGet, Route("users")]
        public async Task<IActionResult> SearchUsers(UserSearchRequest request)
        {
            await PublishSearchQueryLogMessage(request).ConfigureAwait(false);
            SearchResult<UserInfo> result = await _userService.Search(request, GetUserId()).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Push ticket.
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, typeof(object), Description = "All OK")]
        [HttpPost, Route("pushTicket")]
        public ActionResult PushTicket()
        {
            _bus.Publish(TicketCreationMessageFaker.Generate(1).First());
            return Ok();
        }

        /// <summary>
        /// Push user.
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, typeof(object), Description = "All OK")]
        [HttpPost, Route("pushUser")]
        public ActionResult PushUser()
        {
            _bus.Publish(UserCreationMessageFaker.Generate(1).First());
            return Ok();
        }

        private Task PublishSearchQueryLogMessage(TicketSearchRequest request)
        {
            return _bus.PublishAsync(new SearchQueryLogMessage
            {
                UserId = GetUserId(),
                SearchCriterium = request.Name,
                FilterInfo = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                })
            });
        }

        private Task PublishSearchQueryLogMessage(UserSearchRequest request)
        {
            return _bus.PublishAsync(new SearchQueryLogMessage
            {
                UserId = GetUserId(),
                FilterInfo = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore
                })
            });
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