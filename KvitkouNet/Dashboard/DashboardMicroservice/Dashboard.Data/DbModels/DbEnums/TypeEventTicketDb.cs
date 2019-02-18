using System;


namespace Dashboard.Data.DbModels.DbEnums
{
    /// <summary>
    ///     Перечисление, описывающее типы билетов
    /// </summary>
    [Flags]
    public enum TypeEventTicketDb
    {
        /// <summary>
        ///     Тип не установлен
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     Тип кино
        /// </summary>
        Movie = 1,

        /// <summary>
        ///     Тип концерт
        /// </summary>
        Concerts = 2,

        /// <summary>
        ///     Тип театр
        /// </summary>
        Theater = 3,

        /// <summary>
        ///     Тип балет
        /// </summary>
        Ballet = 4,

        /// <summary>
        ///     Тип спорт
        /// </summary>
        Sport = 5,

        /// <summary>
        ///     Тип вечеринка
        /// </summary>
        Parties = 6,

        /// <summary>
        ///     Тип тренинг
        /// </summary>
        Trainings = 7,

        /// <summary>
        ///     Тип выставка
        /// </summary>
        Exhibitions = 8,

        /// <summary>
        ///     Тип цирк
        /// </summary>
        Circus = 9
    }
}
