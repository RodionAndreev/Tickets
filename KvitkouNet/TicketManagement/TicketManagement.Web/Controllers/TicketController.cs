using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TicketManagement.Logic.Models;
using TicketManagement.Logic.Services;

namespace TicketManagement.Web.Controllers
{
    /// <summary>
    ///     Контроллер, упраляющий запросами касающихся билетов
    /// </summary>
    [Route("api/tickets")]
    public class TicketController : Controller
    {
        private readonly ITicketService _service;

        public TicketController(ITicketService service)
        {
            _service = service;
        }

        /// <summary>
        ///     Добавляет билет
        /// </summary>
        /// <param name="ticket">Модель билета</param>
        /// <returns>Код ответа Ok и добавленную модель</returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, typeof(string), Description = "Ticket created")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(string), Description = "User rating is negative")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(string), Description = "Unauthorized user")]
        public async Task<IActionResult> Add([FromBody] Ticket ticket)
        {
            var responseInfo = await _service.Add(ticket);
            return Ok(responseInfo);
        }

        /// <summary>
        ///     Добавление пользователя в "я пойду"
        /// </summary>
        /// <param name="id">id билета</param>
        /// <param name="user">Пользователь добавивший билет</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/add")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "Ticket update")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(string), Description = "Access error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(string), Description = "Unauthorized user")]
        public async Task<IActionResult> AddRespondedUsers([FromRoute] string id, [FromBody] UserInfo user)
        {
            await _service.AddRespondedUsers(id, user);
            return NoContent();
        }

        /// <summary>
        ///     Обновление информации о билете
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ticket">Модель билета</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "Ticket update")]
        [SwaggerResponse(HttpStatusCode.Forbidden, typeof(string), Description = "Access error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(string), Description = "Unauthorized user")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Ticket ticket)
        {
            await _service.Update(id, ticket);
            return NoContent();
        }

        /// <summary>
        ///     Удаление всех билетов
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "Tickets delete")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Error")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(string), Description = "Unauthorized user")]
        public async Task<IActionResult> DeleteAll()
        {
            await _service.DeleteAll();
            return NoContent();
        }

        /// <summary>
        ///     Удаление билета с определенным Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "Ticket delete")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(string), Description = "Ticket not found")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(string), Description = "Unauthorized user")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _service.Delete(id);
            return NoContent();
        }

        /// <summary>
        ///     Получение всех билет имеющихся в системе
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ticket>), Description = "All Ok")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> GetAll()
        {
            var responseInfo = await _service.GetAll();
            return Ok(responseInfo);
        }

        /// <summary>
        ///     Получение билета по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Ticket), Description = "All Ok")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(string), Description = "Ticket not found")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var responseInfo = await _service.Get(id);
            return Ok(responseInfo);
        }

        /// <summary>
        ///     Получение только актуальных билетов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("actual")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Ticket>), Description = "All Ok")]
        public async Task<IActionResult> GetAllActual()
        {
            var responseInfo = await _service.GetAllActual();
            return Ok(responseInfo);
        }

        /// <summary>
        ///     Получение всех билет имеющихся в системе постранично
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("page/{index}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Page<TicketLite>), Description = "All Ok")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(string), Description = "Page not found")]
        public async Task<IActionResult> GetAllPagebyPage([FromRoute] int index)
        {
            var responseInfo = await _service.GetAllPagebyPage(index);
            return Ok(responseInfo);
        }

        /// <summary>
        ///     Получение всех актуальных билетов имеющихся в системе постранично
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("actual/page/{index}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(Page<TicketLite>), Description = "All Ok")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(string), Description = "Page not found")]
        public async Task<IActionResult> GetAllPagebyPageActual([FromRoute] int index)
        {
            var responseInfo = await _service.GetAllPagebyPageActual(index);
            return Ok(responseInfo);
        }
    }
}