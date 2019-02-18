namespace Security.Web.Models
{
    public class EditUserRightsRequest
    {
        public string UserId { get; set; }
        public int[] RoleIds { get; set; }
        public int[] FunctionIds { get; set; }
        public int[] AccessedRightsIds { get; set; }
        public int[] DeniedRightsIds { get; set; }
    }
}
