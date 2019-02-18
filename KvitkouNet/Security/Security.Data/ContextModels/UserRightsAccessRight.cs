namespace Security.Data.ContextModels
{
    /// <summary>
    /// Многие ко многим UserRights и AccessRight
    /// </summary>
    public class UserRightsAccessRight
    {
        public string UserId { get; set; }

        public UserRights UserRights { get; set; }

        public int AccessRightId { get; set; }

        public AccessRight AccessRight { get; set; }

        public bool IsDenied { get; set; }
    }
}
