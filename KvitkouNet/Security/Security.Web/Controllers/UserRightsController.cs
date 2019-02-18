using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Security.Logic.Models.Requests;
using Security.Logic.Models.Responses;
using Security.Logic.Services;
using Security.Web.Models;

namespace Security.Web.Controllers
{
    [Route("api/security")]
    public class UserRightsController : Controller
    {
        private IUserRightsService _securityService;

        public UserRightsController(IUserRightsService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet, Route("users/{per_page:int}/{page:int}/{mask?}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserInfoResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        public async Task<IActionResult> GetUsers(int per_page, int page, string mask)
        {
            var result = _securityService.GetUsersInfo(per_page, page, mask);
            return Ok(await result);
        }

        [HttpGet, Route("rights/user/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(UserRightsResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "Nothing was found on this request")]
        public async Task<IActionResult> GetUserRights(string id)
        {
            var result = _securityService.GetUserRights(id);
            return Ok(await result);
        }

        [HttpPut, Route("rights/user")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ActionResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        public async Task<IActionResult> EditUserRights([FromBody]EditUserRightsRequest request)
        {
            var result = _securityService.EditUserRights(request.UserId, request.RoleIds,
                request.FunctionIds, request.AccessedRightsIds, request.DeniedRightsIds);
            return Ok(await result);
        }

        [HttpPut, Route("rights/check")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AccessResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        public async Task<IActionResult> CheckUserRights([FromBody]CheckAccessRequest accessRequest)
        {
            var result = _securityService.CheckAccess(accessRequest);
            return Ok(await result);
        }
    }
}
