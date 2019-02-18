using System.Threading.Tasks;
using AdminPanel.Logic.Dtos.UserManagement;

namespace AdminPanel.Logic.Infrastructure
{
	/// <summary>
	/// Сервис для работы с пользователями через панель администратора
	/// </summary>
	public interface IUserServiceWrapper
	{
		/// <summary>
		/// Возвращает пользователей
		/// </summary>
		/// <returns></returns>
		Task<object> GetAll();

		/// <summary>
		/// Банит/снимает бан с пользователя
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		Task ChangeIsBannedStatus(IsBannedDto dto);
	}
}