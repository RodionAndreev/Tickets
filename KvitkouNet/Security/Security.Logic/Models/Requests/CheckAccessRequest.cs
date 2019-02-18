namespace Security.Logic.Models.Requests
{
    /// <summary>
    /// Запрос на наличие прав на функцию у пользователя
    /// </summary>
    public class CheckAccessRequest
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Имена запрашиваемых для проверки прав
        /// </summary>
        public string[] AccessRightNames { get; set; }
    }
}
