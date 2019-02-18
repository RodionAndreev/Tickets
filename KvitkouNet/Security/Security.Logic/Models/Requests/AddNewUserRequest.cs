using MediatR;

namespace Security.Logic.Models.Requests
{
    /// <summary>
    /// Добавление нового пользователя
    /// </summary>
    public class AddNewUserRequest : IRequest<bool>
    {
       public UserInfo UserInfo { get; set; }
    }
}
