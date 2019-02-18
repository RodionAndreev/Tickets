namespace StatisticUser.Logic.Interfaces
{
    public interface IUserRating
    {
        int Id { get; set; }
        int Posivite { get; set; }
        int Negative { get; set; }
    }
}