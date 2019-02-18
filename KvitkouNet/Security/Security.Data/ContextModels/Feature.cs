using System.Collections.Generic;

namespace Security.Data.ContextModels
{
    /// <summary>
    /// Фича
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Идентификатор фичи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя фичи
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список прав предоставляемых фичей
        /// </summary>
        public List<FeatureAccessRight> FeatureAccessRight { get; set; }
    }
}
