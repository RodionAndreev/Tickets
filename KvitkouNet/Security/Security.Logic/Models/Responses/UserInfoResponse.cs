namespace Security.Logic.Models.Responses
{
    public class UserInfoResponse : ActionResponse
    {
        /// <summary>
        /// Роли
        /// </summary>
        public UserInfo[] UsersInfo { get; set; }

        public int TotalCount { get; set; }
    }
}
