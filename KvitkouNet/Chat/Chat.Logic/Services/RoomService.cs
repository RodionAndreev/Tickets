using System;
using System.Collections.Generic;
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
    public class RoomService : IRoomService
    {
        private readonly ChatContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;


        public RoomService(ChatContext context, IMapper mapper, IValidator<Room> validator)
        {
            _context = context;
            _mapper = mapper;
          //  _validator = validator;
        }

        public async Task<IEnumerable<Room>> GetRooms(string userId)
        {
            var res = await _context.Rooms.ToArrayAsync();
            return _mapper.Map<IEnumerable<Room>>(res);
        }

        public async Task AddRoom(Room room, string userId)
        {
            //создать можно только одну Main комнату
            if (!_context.Rooms.Any())
            {
                var modelDb = _mapper.Map<RoomDb>(room);
                modelDb.OwnerId = userId;
                await _context.Rooms.AddAsync(modelDb);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Room>> SearchRoom(string template)
        {
            var res = await _context.Rooms.Where(x => EF.Functions.Like(x.Name, $"%{template}%")).ToArrayAsync();
            return _mapper.Map<IEnumerable<Room>>(res);
        }
        // todo нужно добавить отграничение по истории historyCountsMessages  в select
        public async Task<IEnumerable<Message>> GetMessages(string roomId, int historyCountsMessages)
        {
            var res = await _context.Messages.Where(x => x.RoomId == roomId).ToArrayAsync();
            return _mapper.Map<IEnumerable<Message>>(res);
        }

        public async Task<IEnumerable<Message>> SearchMessage(string roomId, string template)
        {
            var res = await _context.Messages.Where(x => x.RoomId == roomId && EF.Functions.Like(x.Text, $"%{template}%")).ToArrayAsync();
            return _mapper.Map<IEnumerable<Message>>(res);
        }

        public async Task<string> AddMessage(Message message, string roomId)
        {
            var modelDb = _mapper.Map<MessageDb>(message);
            modelDb.RoomId = roomId;

            //var maxMessageId = _context.Messages.LastOrDefaultAsync().Result.Id;
            //modelDb.Id = maxMessageId + 1;
            //id автоинкрементируется
            await _context.Messages.AddAsync(modelDb);
            await _context.SaveChangesAsync();
            var ownerId =  _context.Rooms.SingleOrDefaultAsync(x => x.Id == roomId);
            var user = _context.Users.SingleOrDefaultAsync(x => x.Id == ownerId.Result.OwnerId);

            return user.Result.IsOnline ? null :user.Result.Id;
        }

        public async Task EditMessage(Message message, string roomId)
        {
            var modelDb = await _context.Messages.SingleOrDefaultAsync(x => x.Id == message.Id);
            if (modelDb == null)
            {
                await Task.FromException(new InvalidDataException());
            }
            else
            {
                var newModelDb = _mapper.Map<MessageDb>(message);
                modelDb.Text = newModelDb.Text;
                modelDb.IsEdit = true;

                _context.Attach(modelDb);
                _context.Entry(modelDb).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMessage(string messageId)
        {
            var modelDb = await _context.Messages.SingleOrDefaultAsync(x => x.Id == messageId);
            _context.Messages.Remove(modelDb);
            await _context.SaveChangesAsync();
        }

        #region IDisposable Support

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}
