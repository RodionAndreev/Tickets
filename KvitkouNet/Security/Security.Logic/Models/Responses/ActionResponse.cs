using Security.Logic.Models.Enums;

namespace Security.Logic.Models.Responses
{
    /// <summary>
    /// Результат выполнения операции
    /// </summary>
    public class ActionResponse
    {
        /// <summary>
        /// Статус выполнения операции
        /// </summary>
        public ActionStatus Status { get; set; }

        /// <summary>
        /// Дополнительная информация о выполнении
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; set; }
    }
}
