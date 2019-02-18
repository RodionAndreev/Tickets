namespace Security.Data.ContextModels
{
    /// <summary>
    /// Многие ко многим Feature и AccessRight
    /// </summary>
    public class FeatureAccessRight
    {
        public int FeatureId { get; set; }

        public Feature Feature { get; set; }

        public int AccessRightId { get; set; }

        public AccessRight AccessRight { get; set; }
    }
}
