using System.Threading.Tasks;
using UserSettings.Data.DbModels;

namespace UserSettings.Data
{
	public interface ISettingsRepo
	{
		Task<SettingsDb> Get(string id);

		Task<bool> CreateSettings(string id);

		/// <summary>
		/// Обновление информации о том какие уведомления получать 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="notifications"></param>
		/// <returns></returns>
		Task<bool> UpdateNotifications(string id, NotificationDb notifications);

		/// <summary>
		/// Обновление предпочтений по отображению информации
		/// </summary>
		/// <param name="id"></param>
		/// <param name="address"></param>
		/// <param name="region"></param>
		/// <param name="place"></param>
		/// <returns></returns>
		Task<bool> UpdatePreferences(string id, string address, string region, string place);

		/// <summary>
		/// Обновление информации о том какая информация о пользователе доступна для при просмотре профиля
		/// </summary>
		/// <param name="id"></param>
		/// <param name="visibleInfoDb"></param>
		/// <returns></returns>
		Task<bool> UpdateVisible(string id, VisibleInfoDb visibleInfoDb);

		Task<bool> UpdateSettings(string id, bool isPrivate, bool isGetInfo);
	}
}
