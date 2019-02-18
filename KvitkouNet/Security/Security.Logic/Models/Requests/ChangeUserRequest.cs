using MediatR;

namespace Security.Logic.Models.Requests
{
    /// <summary>
    /// Изменение пользователя
    /// </summary>
    public class ChangeUserRequest : IRequest<bool>
    {
       public UserInfo UserInfo { get; set; }
    }
}
