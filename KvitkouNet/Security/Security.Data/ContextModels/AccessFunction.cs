using System.Collections.Generic;

namespace Security.Data.ContextModels
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
        /// Фича
        /// </summary>
        public Feature Feature { get; set; }

        /// <summary>
        /// Список прав предоставляемых функцией
        /// </summary>
        public List<AccessFunctionAccessRight> AccessFunctionAccessRights { get; set; }

        /// <summary>
        /// Список прав
        /// </summary>
        public List<RoleAccessFunction> RoleAccessFunction { get; set; }

        /// <summary>
        /// Список предоставленных пользователю функций
        /// </summary>
        public List<UserRightsAccessFunction> UserRightsAccessFunction { get; set; }
    }
}
