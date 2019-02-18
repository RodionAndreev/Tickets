namespace Security.Data.ContextModels
{
    /// <summary>
    /// Многие ко многим UserRights и Role
    /// </summary>
    public class UserRightsRole
    {
        public string UserId { get; set; }

        public UserRights UserRights { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
