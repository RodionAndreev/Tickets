using AutoMapper;
using EasyNetQ;
using FluentValidation;
using KvitkouNet.Messages.UserManagement;
using KvitkouNet.Messages.UserSettings;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using UserSettings.Data;
using UserSettings.Data.DbModels;
using UserSettings.Logic.Models;
using UserSettings.Logic.Models.Helper;

namespace UserSettings.Logic.Services
{
	public class UserSettingsService : IUserSettingsService
	{
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		private readonly IMapper _mapper;
		private readonly IValidator<Settings> _validator;
		private readonly ISettingsRepo _context;
		private readonly IBus _bus;

		public UserSettingsService(IMapper mapper, ISettingsRepo context, IValidator<Settings> validator, IBus bus)
		{
			_mapper = mapper;
			_validator = validator;
			_context = context;
			_bus = bus;
		}

		public async Task<Settings> Get(string id)
		{
			var res =  await _context.Get(id);
			return _mapper.Map<Settings>(res);
		}

		public async Task<ResultEnum> UpdateEmail(string id, string email)
		{
			try
			{
				MailAddress m = new MailAddress(email);
				
				if (await CheckExistEmail(email))
				{
					return ResultEnum.Success;
				}
				else
				{
					return ResultEnum.Error;
				}
			}
			catch
			{
				return ResultEnum.BadRequest;
			}
		}

		public async Task<ResultEnum> UpdatePassword(string id, string current, string newPass, string confirm)
		{
			if(String.Equals(newPass, confirm))
			{
				await _bus.PublishAsync(new PasswordUpdateMessage(current, newPass) { UserId = id });
				return ResultEnum.Success;
			}
			return ResultEnum.BadRequest;
		}

		public async Task<ResultEnum> UpdateProfile(string id, string first, string middle, string last, DateTime birthdate)
		{
			if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(last))
				return ResultEnum.BadRequest;
			await _bus.PublishAsync(new UserProfileUpdateMessage()
			{
				FirstName = first,
				LastName = last,
				MiddleName = middle,
				Birthday = birthdate,
				UserId = id
			});
			return ResultEnum.Success;
		}

		private async Task<bool> CheckExistEmail(string email)
		{
			await _bus.PublishAsync(new EmailUpdateMessage()
			{
				Email = email
			});
			return true;
		}

		public async Task<ResultEnum> UpdateNotifications(string id, Notifications notifications)
		{
			if(await _context.UpdateNotifications(id, _mapper.Map<Notifications, NotificationDb>(notifications)))
			{
				return ResultEnum.Success;
			}
			return ResultEnum.Error;
		}

		public async Task<bool> DeleteAccount(string id)
		{
			await _bus.PublishAsync(new DeleteUserProfileMessage());
			return true;
		}

		public async Task<ResultEnum> UpdateVisible(string id, VisibleInfo visibleInfo)
		{
			if (await _context.UpdateVisible(id, _mapper.Map<VisibleInfo, VisibleInfoDb>(visibleInfo)))
			{
				return ResultEnum.Success;
			}
			return ResultEnum.Error;
		}

		public async Task<ResultEnum> CreateSetting(string id)
		{
			if(await _context.CreateSettings(id))
			{
				return ResultEnum.Success;
			}
			return ResultEnum.Error;
		}

		public async Task<ResultEnum> UpdateSettings(string id, bool isPrivate, bool isGetInfo)
		{
			if (await _context.UpdateSettings(id, isPrivate, isGetInfo))
			{
				return ResultEnum.Success;
			}
			return ResultEnum.Error;
		}

	}
}
