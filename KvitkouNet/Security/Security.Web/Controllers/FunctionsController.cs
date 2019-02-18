using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Security.Logic.Models;
using Security.Logic.Models.Responses;
using Security.Logic.Services;
using Security.Web.Models;

namespace Security.Web.Controllers
{
    [Route("api/security")]
    public class FunctionsController : Controller
    {
        private IFunctionService _securityService;

        public FunctionsController(IFunctionService securityService)
        {
            _securityService = securityService;
        }

        [HttpGet, Route("functions{per_page:int}/{page:int}/{mask?}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AccessFunctionResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        public async Task<IActionResult> GetFunctions(int per_page, int page, string mask)
        {
            var result = _securityService.GetFunctions(per_page, page, mask);
            return Ok(await result);
        }

        [HttpPost, Route("function")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ActionResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        public async Task<IActionResult> AddFunction([FromBody]AddFunctionRequest request)
        {
            var result = _securityService.AddFunction(request.FunctionName, request.FeatureId);
            return Ok(await result);
        }

        [HttpDelete, Route("function/{id:int}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ActionResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        public async Task<IActionResult> DeleteFunction(int id)
        {
            var result = _securityService.DeleteFunction(id);
            return Ok(await result);
        }

        [HttpPut, Route("function")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ActionResponse), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access denied")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        public async Task<IActionResult> EditFunction([FromBody]EditFunctionRequest request)
        {
            var result = _securityService.EditFunctionRights(request.FunctionId, request.RightIds);
            return Ok(await result);
        }
    }
}
