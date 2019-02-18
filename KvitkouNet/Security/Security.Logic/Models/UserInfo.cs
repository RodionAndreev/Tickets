namespace Security.Logic.Models
{
    public class UserInfo
    {
        /// <summary>
        /// Идентификатор пользователя (null для незарегистрированного пользователя)
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }
    }
}
