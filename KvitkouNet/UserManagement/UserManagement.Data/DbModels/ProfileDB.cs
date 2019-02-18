using System;
using System.Collections.Generic;
using UserManagement.Data.DbModels.UserSettings;
using UserManagement.Data.DbModels.Enums;

namespace UserManagement.Data.DbModels
{
    public class ProfileDB
    {
        /// <summary>
        /// Уникальный идентификатор профиля пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public SexDB Sex { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime Birthday { get; set; }


        #region Поля для редактирования администратором 
        /// <summary>
        /// Рейтинг пользователя
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Статус блокировки
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Пометка на удаление пользователя
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// Проверен ли пользователь администратором
        /// </summary>
        public bool IsVerified { get; set; }
        #endregion

        #region Связи между таблицами  
        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual UserDB UserDB { get; set; }

        public string UserDBId { get; set; }

        /// <summary>
        /// Настройки пользователя
        /// </summary>
        //public virtual ProfileSettings ProfileSettings { get; set; }

        /// <summary>
        /// Список адресов пользователя 
        /// </summary>
        public virtual ICollection<AddressDB> Addresses { get; set; }

        /// <summary>
        /// Список телефонов пользователя
        /// </summary>
        public virtual ICollection<PhoneNumberDB> PhoneNumbers { get; set; }
        #endregion
    }
}

