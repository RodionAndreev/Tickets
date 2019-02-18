using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Logic.Models.Enums
{
    
        /// <summary>
        ///     Перечисление, описывающее типы билетов
        /// </summary>
        [Flags]
        public enum TypeEventTicket
        {
            /// <summary>
            ///     Тип не установлен
            /// </summary>
            Unknown = 0,

            /// <summary>
            ///     Тип кино
            /// </summary>
            Movie = 1 << 0,

            /// <summary>
            ///     Тип концерт
            /// </summary>
            Concerts = 1 << 1,

            /// <summary>
            ///     Тип театр
            /// </summary>
            Theater = 1 << 2,

            /// <summary>
            ///     Тип балет
            /// </summary>
            Ballet = 1 << 3,

            /// <summary>
            ///     Тип спорт
            /// </summary>
            Sport = 1 << 4,

            /// <summary>
            ///     Тип вечеринка
            /// </summary>
            Parties = 1 << 5,

            /// <summary>
            ///     Тип тренинг
            /// </summary>
            Trainings = 1 << 6,

            /// <summary>
            ///     Тип выставка
            /// </summary>
            Exhibitions = 1 << 7,

            /// <summary>
            ///     Тип цирк
            /// </summary>
            Circus = 1 << 8
        }
}
