namespace TicketManagement.Data.DbModels.DbEnums
{
    /// <summary>
    ///     Перечисление, описывающее статус билета
    /// </summary>
    public enum TicketStatusDb
    {
        /// <summary>
        ///     Тип не установлен
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Куплен
        /// </summary>
        Purchased = 1,

        /// <summary>
        ///     Актуален
        /// </summary>
        Actual = 2,

        /// <summary>
        ///     Просрочен
        /// </summary>
        Expired = 3
    }
}