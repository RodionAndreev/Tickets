namespace Chat.Logic.Models
{
    public class User
    {
        /// <summary>
        /// UserId пользователя из UserManagement.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Признак пользователя в Online.
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// Avatar пользователя в чате (Url к картинке на диске).
        /// </summary>
        public string Avatar { get; set; }
    }
}
