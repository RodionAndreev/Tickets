namespace Security.Data.ContextModels
{
    /// <summary>
    /// Многие ко многим UserRights и AccessFunction
    /// </summary>
    public class UserRightsAccessFunction
    {
        public string UserId { get; set; }

        public UserRights UserRights { get; set; }

        public int AccessFunctionId { get; set; }

        public AccessFunction AccessFunction { get; set; }
    }
}
