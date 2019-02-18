using System;
using System.Threading.Tasks;
using UserSettings.Logic.Models;
using UserSettings.Logic.Models.Helper;

namespace UserSettings.Logic.Services
{
	/// <summary>
	/// Сервис для обновления настроек пользователя
	/// </summary>
	public interface IUserSettingsService : IDisposable
	{
		/// <summary>
		/// Обновление профиля.
		/// </summary>
		/// <param name="profile"></param>
		/// <returns></returns>
		Task<ResultEnum> UpdateProfile(string id, string first, string middle, string last, DateTime birthdate);

		/// <summary>
		/// Обновление пароля.
		/// </summary>
		/// <param name="current"></param>
		/// <param name="newPass"></param>
		/// <param name="confirm"></param>
		/// <returns></returns>
		Task<ResultEnum> UpdatePassword(string id, string current, string newPass, string confirm);

		/// <summary>
		/// Обновление почты.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		Task<ResultEnum> UpdateEmail(string id, string email);

		Task<Settings> Get(string id);

		Task<ResultEnum> UpdateNotifications(string id, Notifications notifications);

		Task<bool> DeleteAccount(string id);
		Task<ResultEnum> UpdateVisible(string id, VisibleInfo visibleInfo);

		Task<ResultEnum> CreateSetting(string id);

		Task<ResultEnum> UpdateSettings(string id, bool isPrivate, bool isGetInfo);
	}
}
