
namespace UserManagement.Data.DbModels.Tickets
{
    /// <summary>
    ///     Класс описания доменной модели билета
    /// </summary>
    public class TicketDB
    {
        public string Id { get; set; }

        /// <summary>
        ///     Пользователи, которые добавили билет в “Я Пойду”
        /// </summary>
        //public List<UserDB> RespondedUsers { get; set; }

        /// <summary>
        ///     Платный/бесплатный билет
        /// </summary>
        public bool Free { get; set; } 

        /// <summary>
        ///     Название билета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Id билета
        /// </summary>
        public string TicketId { get; set; }

        /// <summary>
        ///     Пользователь разместивший билет
        /// </summary>
        public virtual UserDB User { get; set; }

        public string UserDBId { get; set; }
    }
}
