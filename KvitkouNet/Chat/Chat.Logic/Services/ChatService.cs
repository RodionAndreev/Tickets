using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chat.Data.Context;
using Chat.Data.DbModels;
using Chat.Logic.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Chat.Logic.Services
{
    public class ChatService : IChatService
    {
        private readonly ChatContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;

        public ChatService(ChatContext context, IMapper mapper, IValidator<Settings> validator)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddUser(User newUser)
        {
            var modelDb = _mapper.Map<UserDb>(newUser);
            await _context.Users.AddAsync(modelDb);

            //добавим дефолтные настрйоки для пользователя
            await _context.Settings.AddAsync(new SettingsDb()
            {
                BackgroundColor =  BackgroundColorType.Black,
                DisablePrivateMessages = false,
                HideChat = false,
                HistoryCountsMessages = 15,
                Sound = false,
                Tab = false,
                Toast = false,
                UpdateDate = DateTime.Now,
                UserId = modelDb.Id,
                ViewTimestampsMessage = false
            });
            await _context.SaveChangesAsync();
        }

        public async Task EditUser(User user)
        {

            var res = await _context.Settings.SingleOrDefaultAsync(x => x.UserId == user.Id);
            if (res == null)
            {
                await Task.FromException(new InvalidDataException());
            }
            else
            {
                var modelDb = _mapper.Map<UserDb>(user);
                modelDb.UserName = user.UserName;
                _context.Attach(modelDb);
                _context.Entry(modelDb).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Settings> GetUserSettings(string userId)
        {
            var res = await _context.Settings.SingleOrDefaultAsync(x => x.UserId == userId);
            return res == null ? null : _mapper.Map<Settings>(res);
        }

        public async Task EditUserSettings(string userId, Settings settings)
        {
           
            var modelDb = await _context.Settings.SingleOrDefaultAsync(x => x.UserId == userId);
            if (modelDb == null)
            {
                await Task.FromException(new InvalidDataException());
            }
            else
            {
                var newModelDb = _mapper.Map<SettingsDb>(settings);
                modelDb.BackgroundColor = newModelDb.BackgroundColor;
                modelDb.DisablePrivateMessages = newModelDb.DisablePrivateMessages;
                modelDb.HideChat = newModelDb.HideChat;
                modelDb.HistoryCountsMessages = newModelDb.HistoryCountsMessages;
                modelDb.Sound = newModelDb.Sound;
                modelDb.Tab = newModelDb.Tab;
                modelDb.Toast = newModelDb.Toast;
                modelDb.ViewTimestampsMessage = newModelDb.ViewTimestampsMessage;
                modelDb.UpdateDate = DateTime.Now;
                
                _context.Attach(modelDb);
               _context.Entry(modelDb).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        #region IDisposable Support

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}
