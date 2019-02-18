namespace Security.Logic.Models.Responses
{
    public class RoleResponse : ActionResponse
    {
        /// <summary>
        /// Роли
        /// </summary>
        public Role[] Roles { get; set; }

        public int TotalCount { get; set; }
    }
}
