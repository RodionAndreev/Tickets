using Logging.Data.DbModels.Abstraction;

namespace Logging.Data.DbModels
{
    /// <summary>
    /// Запись в лог, описывающая действие с аккаунтом пользователя
    /// </summary>
    public class AccountLogEntryDbModel : BaseLogEntryDbModel
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Почтовый адрес пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Тип действия
        /// </summary>
        public int Type { get; set; }
    }
}
