namespace Security.Web.Models
{
    public class EditRoleRequest
    {
        public int RoleId { get; set; }
        public int[] FunctionIds { get; set; }
        public int[] AccessRightsIds { get; set; }
        public int[] DeniedRightsIds { get; set; }
    }
}
