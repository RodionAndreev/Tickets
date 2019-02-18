using Chat.Data.DbModels;

namespace Chat.Logic.Models
{
    public class Settings
    {
        /// <summary>
        /// Уникальный Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Фон чата
        /// </summary>
        public BackgroundColorType BackgroundColor { get; set; }

        /// <summary>
        /// Звуковое уведомление. false - выкл. default - true.
        /// </summary>
        public bool Sound { get; set; }

        /// <summary>
        /// Всплывающие уведомление. false - выключено. default - true.
        /// </summary>
        public bool Toast { get; set; }

        /// <summary>
        /// Открыть чат в новой вкладке или в основной. default - false - В основной.
        /// </summary>
        public bool Tab { get; set; }

        /// <summary>
        /// Отображать ли время сообщения. default - false - Не отображать время сообщения.
        /// </summary>
        public bool ViewTimestampsMessage { get; set; }

        /// <summary>
        /// Скрытость чата. default - false - Не скрывать чат.
        /// </summary>
        public bool HideChat { get; set; }

        /// <summary>
        /// Настройка отображения колличества сообщений из истории чата. default - 100 сообщений
        /// </summary>
        public int HistoryCountsMessages { get; set; }

        /// <summary>
        /// Признак запрета для получения личных сообщений от других пользователей. default - false.
        /// </summary>
        public bool DisablePrivateMessages { get; set; }
    }
}

