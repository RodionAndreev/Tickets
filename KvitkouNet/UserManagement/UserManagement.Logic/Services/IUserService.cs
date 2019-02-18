using KvitkouNet.Messages.UserSettings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Logic.Models;

namespace UserManagement.Logic.Services
{
    public interface IUserService: IDisposable
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> Register(UserRegisterModel model);

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        IEnumerable<ForViewModel> GetAll();

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ForViewModel>> GetAllAsync();

        /// <summary>
        /// Получение пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ForViewModel> Get(string id);

        /// <summary>
        /// Получение пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<ModelWithHashPassw> GetByLogin(string login);

        /// <summary>
        /// Обновление пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<string> Update(string id, ForUpdateModel userModel);

        /// <summary>
        /// Обновление пользователя по login
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<string> UpdateByLogin(string id, ForUpdateModel userModel);

        /// <summary>
        /// Обновление почтового адреса
        /// </summary>
        /// <param name="emailUpdateMessage"></param>
        /// <returns></returns>
        Task<bool> UpdateEmail(EmailUpdateMessage emailUpdateMessage);

        /// <summary>
        /// Обновление почтового адреса
        /// </summary>
        /// <param name="emailUpdateMessage"></param>
        /// <returns></returns>
        Task<bool> UpdateEmailStatus(string login);

        /// <summary>
        /// Удаление пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> Delete(string id);

        /// <summary>
        /// Нахождение Email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> GetEmail(string email);

        /// <summary>
        /// Добавление группы
        /// </summary>
        /// <param name="userGroupModel"></param>
        /// <returns></returns>
        Task<string> AddGroup(GroupModel userGroupModel);

        /// <summary>
        /// Получение всех групп
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<GroupModel>> GetAllGroups();

        /// <summary>
        /// Получение группы по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GroupModel> GetGroupById(int id);

        /// <summary>
        /// Обновление группы по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GroupModel> UpdateGroupById(int id);

        /// <summary>
        /// Удаление группы по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> DeleteGroupById(int id);

        /// <summary>
        /// Получение всех пользователей состоящих в группе с id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<ForViewModel>> GetAllUsersInGroupById(int id);
    }
}