using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Logic.Models;
using UserManagement.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace UserManagement.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с группами пользователей
    /// </summary>
    [Route("api/groups")]
    public class UserGroupController : Controller
    {
        private IUserService _service;

        public UserGroupController(IUserService service)
        {
            _service = service;
        }
        /// <summary>
        /// Добавление группы
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(object), Description = "Group Created")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> Add([FromBody] GroupModel userGroupModel)
        {
            var result = await _service.AddGroup(userGroupModel);
            return Ok(result);
        }

        /// <summary>
        /// Получение всех групп пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<GroupModel>), Description = "All Ok")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllGroups();
            return Ok(result);
        }

        /// <summary>
        /// Получение группы по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(bool), Description = "Group is returned")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid id")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetGroupById(id);
            return Ok(result);
        }

        /// <summary>
        /// Обновление группы по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut, Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(bool), Description = "Group updated")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] GroupModel userModel)
        {
            var result = await _service.UpdateGroupById(id);
            return Ok(result);
        }

        /// <summary>
        /// Удаление группы по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Route("{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(bool), Description = "Group delete")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid login")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var result = await _service.DeleteGroupById(id);
            return Ok(result);
        }


        /// <summary>
        /// Получение всех пользователей группы
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("{id}/allusers")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<ForViewModel>), Description = "All Ok")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
        public async Task<IActionResult> GetAllUsersInGroupById(int id)
        {
            var result = await _service.GetAllUsersInGroupById(id);
            return Ok(result);
        }
    }
}