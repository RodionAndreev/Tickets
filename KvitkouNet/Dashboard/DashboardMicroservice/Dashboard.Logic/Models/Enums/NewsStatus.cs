
namespace Dashboard.Logic.Models.Enums
{
    /// <summary>
    ///     Перечисление, описывающее статус новости
    /// </summary>
    public enum NewsStatus
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
