
using StatisticUser.Data.DbModels.AbstractionsDB;

namespace StatisticUser.Data.DbModels
{
    /// <summary>
    /// Содержит данные регистраций по времени
    /// Планируется:
    /// Каждый час создается запись и сумируются
    /// новые регистрации пользователей.
    /// </summary>
    class RegistrationTimeDB: Entity<int>
    {
        public int NumberRegistrations { get; set; } = 0;
    }
}
