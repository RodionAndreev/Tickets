using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Dashboard.Logic.Models;
using Dashboard.Logic.Models.Enums;
using Dashboard.Logic.Services;

namespace Dashboard.Web.Controllers
{
    /// <summary>
    ///     Контроллер, упраляющий запросами новостей
    /// </summary>
    [Route("api/news")]
    public class NewsController : Controller
    {
        private readonly IDashboardService _service;

        public NewsController(IDashboardService service)
        {
            _service = service;
        }

        /// <summary>
        ///     Добавляет новость
        /// </summary>
        /// <param name="news">Модель новости</param>
        /// <returns>Код ответа Create и добавленную модель</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(string), Description = "News created")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> Add([FromBody] News news)
        {
            var result = await _service.Add(news);
            if (result.Item2 == RequestStatus.BadRequest) return BadRequest();
            if (result.Item2 == RequestStatus.Error) return StatusCode(500);
            return Ok(result.Item1);
        }


        /// <summary>
        ///     Получение всех новостей имеющихся в системе
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<News>), Description = "All Ok")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();

            if (result.Item2 != RequestStatus.Success)
                return BadRequest();

            return Ok(result);
        }

        /// <summary>
        ///     Получение новости по Id
        /// </summary>
        /// <param name="NewsId">Id новости</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{NewsId}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(News), Description = "All Ok")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Access error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> Get([FromRoute] string NewsId)
        {
            var result = await _service.Get(NewsId);
            return Ok(result);
        }

        /// <summary>
        ///     Удаление новости с определенным Id
        /// </summary>
        /// <param name="NewsId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{NewsId}")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(News), Description = "News delete")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "News not found")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(string), Description = "Unauthorized user")]
        public IActionResult Delete([FromRoute] string NewsId)
        {
            var result = _service.Delete(NewsId);
            return Ok();
        }

    }
}