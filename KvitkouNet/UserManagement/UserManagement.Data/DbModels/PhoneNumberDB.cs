namespace UserManagement.Data.DbModels
{
    public class PhoneNumberDB
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }

        #region Связи между таблицами  
        /// <summary>
        /// Профиль пользователя
        /// </summary>
        public virtual ProfileDB Profile { get; set; }
        #endregion
    }
}