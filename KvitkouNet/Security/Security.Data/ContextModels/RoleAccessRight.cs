namespace Security.Data.ContextModels
{
    /// <summary>
    /// Многие ко многим Role и AccessRight
    /// </summary>
    public class RoleAccessRight
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int AccessRightId { get; set; }

        public AccessRight AccessRight { get; set; }

        public bool IsDenied { get; set; }
    }
}
