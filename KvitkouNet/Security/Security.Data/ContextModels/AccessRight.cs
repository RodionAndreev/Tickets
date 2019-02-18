using System.Collections.Generic;

namespace Security.Data.ContextModels
{
    /// <summary>
    /// Право доступа
    /// </summary>
    public class AccessRight
    {
        /// <summary>
        /// Идентификатор права доступа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя права доступа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список прав предоставляемых функцией
        /// </summary>
        public List<AccessFunctionAccessRight> AccessFunctionAccessRight { get; set; }

        /// <summary>
        /// Список прав предоставляемых фичей
        /// </summary>
        public List<FeatureAccessRight> FeatureAccessRight { get; set; }

        /// <summary>
        /// Список прав
        /// </summary>
        public List<RoleAccessRight> RoleAccessRight { get; set; }

        /// <summary>
        /// Список прав
        /// </summary>
        public List<UserRightsAccessRight> UserRightsAccessRight { get; set; }
    }
}
