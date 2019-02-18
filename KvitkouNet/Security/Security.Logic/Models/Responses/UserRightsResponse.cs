namespace Security.Logic.Models.Responses
{
    public class UserRightsResponse : ActionResponse
    {
        /// <summary>
        /// Права пользователя
        /// </summary>
        public UserRights UserRights { get; set; }
    }
}
