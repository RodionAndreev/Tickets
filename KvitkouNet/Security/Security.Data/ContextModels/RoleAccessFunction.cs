namespace Security.Data.ContextModels
{
    /// <summary>
    /// Многие ко многим Role и AccessFunction
    /// </summary>
    public class RoleAccessFunction
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int AccessFunctionId { get; set; }

        public AccessFunction AccessFunction { get; set; }
    }
}
