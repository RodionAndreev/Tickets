using Security.Data.Exceptions;

namespace Security.Logic.Helpers
{
    /// <summary>
    /// Helper обработки ошибок
    /// </summary>
    static class PrettyExceptionHelper
    {
        /// <summary>
        /// Преобразовывает SecurityDbException в строку
        /// </summary>
        /// <param name="e">SecurityDbException</param>
        /// <returns>Типизированное сообщение об ошибке</returns>
        public static string GetMessage(SecurityDbException e)
        {
            var entityName = string.Empty;

            switch (e.EntityType)
            {
                case EntityType.Right:
                    entityName = "Access Right";
                    break;
                case EntityType.Function:
                    entityName = "Access Function";
                    break;
                case EntityType.Feature:
                    entityName = "Feature";
                    break;
                case EntityType.Role:
                    entityName = "Role";
                    break;
                case EntityType.UserRights:
                    entityName = "User Rights";
                    break;
            }
            switch (e.ExceptionType)
            {
                case ExceptionType.NameExists:
                    return $"Names: {string.Join(",", e.Items)} of {entityName} already exist";
                case ExceptionType.NotFound:
                    return $"{entityName} with id = {string.Join(",", e.Items)} was not found";
            }

            return "Unknown code of error";
        }
    }
}
