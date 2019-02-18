namespace StatisticUser.Logic.Interfaces
{
    /// <summary>
    /// Кол-во сообщений пользователя
    /// </summary>
    public interface IUserMessages
    {
       int Id { get; set; }
       int CountMessages { get; set; }
    }
}