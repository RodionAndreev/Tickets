using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using StatisticUser.Logic.DTOs;
using StatisticUser.Logic.Interfaces;
using StatisticUser.Logic.Services;

namespace StatisticUser.Web.Controllers
{
    [Route("api/statistic/user")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        IStatisticUserService _statisticService;

        public StatisticController(IStatisticUserService statisticService)
        {
            _statisticService = statisticService;
        }

        /// <summary>
        /// возвращает cтатистику посещения ресурсов сайта
        /// </summary>
        [HttpPost]
        [Route("resources")]
        [SwaggerTag("Статистика посещения ресурсов сайта")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<ITimeOnResouces>), Description = "Statistics of site resources visiting")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> GetTimeOnResouces([FromBody]DateRange model)
        {
            var result = await _statisticService.GetTimeOnResouces(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("all")]
        [SwaggerTag("Статистика по всем пользователям")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<ITimeOnResouces>), Description = "Statistics of users")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public ActionResult<LoadResult> All([FromQuery] DataSourceLoadOptions loadOptions)
        {
            var  result = _statisticService.GetAllUser(loadOptions);
            return result;
        }

    }
}
