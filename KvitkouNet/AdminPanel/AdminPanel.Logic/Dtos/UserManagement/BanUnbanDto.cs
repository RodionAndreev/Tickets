namespace AdminPanel.Logic.Dtos.UserManagement
{
	/// <summary>
	/// Модель для Patch-запроса для изменения бана/разбана пользователя
	/// </summary>
	public class IsBannedDto
	{
		public int Id { get; set; }

		public bool IsNeedBan { get; set; }
	}
}