using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using KvitkouNet.Messages.UserSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using UserManagement.Data.Context;
using UserManagement.Logic.Models;
using UserManagement.Logic.Services;

namespace UserManagement.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями
    /// </summary>
    [Route("api/users")]
    public class UserController : Controller
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        
        [HttpPost, Route("register")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(string), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> Register([FromBody]UserRegisterModel model)
        {
            var result = await _service.Register(model);
            if (result!="Ok")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<ForViewModel>), Description = "All Ok")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получение пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ForViewModel), Description = "User is returned")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid id")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _service.Get(id);
            return Ok(result);
        }

        /// <summary>
        /// Редактирование пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut, Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(string), Description = "User updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> Update(string id, [FromBody] ForUpdateModel userModel)
        {
            var result = await _service.Update(id, userModel);
            return Ok(result);
        }

        /// <summary>
        /// Удаление пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(string), Description = "User delete")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid login")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }

        /// <summary>
        /// Получение пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet, Route("{login:maxlength(20)}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(ModelWithHashPassw), Description = "User is returned")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid login")]
        public async Task<IActionResult> GetByLogin(string login)
        {
            var result = await _service.GetByLogin(login);
            return Ok(result);
        }

        /// <summary>
        /// Редактирование пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPut, Route("{login:maxlength(20)}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(bool), Description = "User updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> UpdateByLogin(string login, [FromBody] ForUpdateModel userModel)
        {
            var result = await _service.UpdateByLogin(login, userModel);
            return Ok(result);
        }

        /// <summary>
        /// Нахождение Email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("email")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(bool), Description = "User is returned")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(void), Description = "Requires authentication")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid id")]
        public async Task<IActionResult> UpdateEmail([FromBody] EmailUpdateMessage emailUpdateMessage)
        {
            var result = await _service.UpdateEmail(emailUpdateMessage);
            return Ok(result);
        }
    }
}