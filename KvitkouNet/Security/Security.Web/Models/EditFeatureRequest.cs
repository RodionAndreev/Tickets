namespace Security.Web.Models
{
    public class EditFeatureRequest
    {
        public int FeatureId { get; set; }
        public int[] RightsIds { get; set; }
    }
}
