
namespace Dashboard.Data.DbModels.DbEnums
{
    /// <summary>
    ///     Перечисление, описывающее статус новости
    /// </summary>
    public enum NewsStatusDb
    {
        /// <summary>
        ///     Тип не установлен
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Обыкновенная
        /// </summary>
        Delivery = 1,

        /// <summary>
        ///     Горящая
        /// </summary>
        Hot = 2
    }
}

