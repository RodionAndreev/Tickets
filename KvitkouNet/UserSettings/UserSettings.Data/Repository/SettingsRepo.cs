using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using UserSettings.Data.Context;
using UserSettings.Data.DbModels;

namespace UserSettings.Data
{
	/// <summary>
	/// Репозиторий работы с бд
	/// </summary>
	public class SettingsRepo : ISettingsRepo
	{
		private readonly SettingsContext _context;
		public SettingsRepo(SettingsContext context)
		{
			_context = context;
		}

		public async Task<SettingsDb> Get(string id)
		{
			var result = await _context.Settings.FirstOrDefaultAsync(x => x.SettingsId == id);
			_context.Notifications.Load();
			_context.VisibleInformations.Load();
			return result;
		}

		public async Task<bool> UpdateNotifications(string id, NotificationDb notifications)
		{
			var origin = await _context.Settings.FirstOrDefaultAsync(x => x.SettingsId == id);
			_context.Notifications.Load();
			if (origin != null)
			{
				origin.Notifications.IsLikeMyTicket = notifications.IsLikeMyTicket;
				origin.Notifications.IsWantBuy = notifications.IsWantBuy;
				origin.Notifications.IsOtherNotification = notifications.IsOtherNotification;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public Task<bool> UpdatePreferences(string id, string address, string region, string place)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateVisible(string id, VisibleInfoDb visibleInfoDb)
		{
			var origin = await _context.Settings.FirstOrDefaultAsync(x => x.SettingsId == id);
			_context.VisibleInformations.Load();
			if (origin != null)
			{
				origin.VisibleInfo.VisibleAllPhones = visibleInfoDb.VisibleAllPhones;
				origin.VisibleInfo.VisibleEmail = visibleInfoDb.VisibleEmail;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<bool> CreateSettings(string id)
		{
			var origin = await _context.Settings.FirstOrDefaultAsync(x => x.SettingsId == id);
			if (origin != null) return false;
			await _context.Settings.AddAsync(new SettingsDb()
			{
				SettingsId = id,
				IsGetTicketInfo = false,
				IsPrivateAccount = false,
				PreferAddress = "",
				PreferPlace = "",
				PreferRegion = "",
				Notifications = new NotificationDb()
				{
					IsLikeMyTicket = false,
					IsWantBuy = false,
					IsOtherNotification = false,
				},
				VisibleInfo = new VisibleInfoDb()
				{
					VisibleAllPhones = false,
					VisibleEmail = false
				}
			});
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateSettings(string id, bool isPrivate, bool isGetInfo)
		{
			var origin = await _context.Settings.FirstOrDefaultAsync(x => x.SettingsId == id);
			if (origin != null)
			{
				origin.IsGetTicketInfo = isGetInfo;
				origin.IsPrivateAccount = isPrivate;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
