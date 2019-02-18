namespace Security.Logic.Models.Responses
{
    public class FeatureResponse : ActionResponse
    {
        /// <summary>
        /// Фичи
        /// </summary>
        public Feature[] Features { get; set; }
        
        public int TotalCount { get; set; }
    }
}
