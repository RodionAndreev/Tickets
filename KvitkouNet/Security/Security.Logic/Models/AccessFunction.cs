using System.Collections.Generic;

namespace Security.Logic.Models
{
    /// <summary>
    /// Функция доступа к фиче
    /// </summary>
    public class AccessFunction
    {
        /// <summary>
        /// Идентификатор функции
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя функции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор фичи
        /// </summary>
        public int FeatureId { get; set; }

        /// <summary>
        /// Имя фичи
        /// </summary>
        public string FeatureName { get; set; }

        /// <summary>
        /// Список прав предоставляемых функцией
        /// </summary>
        public List<AccessRight> AccessRights { get; set; }
    }
}
