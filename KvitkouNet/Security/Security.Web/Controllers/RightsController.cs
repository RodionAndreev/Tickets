using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Security.Logic.Models;
using Security.Logic.Models.Responses;
using Security.Logic.Services;

namespace Security.Web.Controllers
{
    [Route("api/security")]
    public class RightsController : Controller
    {
        private IRightsService _securityService;

        public RightsController(IRightsService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet, Route("rights/{per_page:int}/{page:int}/{mask?}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AccessRightResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        public async Task<IActionResult> GetRights(int per_page, int page, string mask)
        {
            var result = _securityService.GetRights(per_page, page, mask);
            return Ok(await result);
        }

        [HttpPost, Route("rights")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AccessRightResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        public async Task<IActionResult> AddRights([FromBody]string[] rightNames)
        {
            var result = _securityService.AddRights(rightNames);
            return Ok(await result);
        }

        [HttpDelete, Route("right/{id:int}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ActionResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        public async Task<IActionResult> DeleteRight(int id)
        {
            var result = _securityService.DeleteRight(id);
            return Ok(await result);
        }
    }
}
