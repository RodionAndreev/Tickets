using AutoMapper;
using EasyNetQ;
using FluentValidation;
using KvitkouNet.Messages.Logging;
using KvitkouNet.Messages.Logging.Enums;
using KvitkouNet.Messages.UserManagement;
using KvitkouNet.Messages.UserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data.Context;
using UserManagement.Data.DbModels;
using UserManagement.Data.Repositories;
using UserManagement.Logic.Models;


namespace UserManagement.Logic.Services
{
    class UserService : IUserService
    {
        
        private readonly IMapper _mapper;
        private readonly IValidator<UserRegisterModel> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserContext _context;
        private readonly IBus _bus;

        public UserService(IMapper mapper, IValidator<UserRegisterModel> validator, IUnitOfWork unitOfWork, IBus bus, UserContext context)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _bus = bus;
        }

        public async Task<string> Register(UserRegisterModel model)
        {
            var result = _validator.Validate(model);
            if (!result.IsValid) return result.Errors.First().ToString();
            var findLogin = _unitOfWork.Accounts.FindAsync(x => x.Login == model.UserName).Result.FirstOrDefault();
            if (findLogin!=null)
            {
                return "Sorry, this username allready exist!";
            }
            var findEmail = _unitOfWork.Accounts.FindAsync(x => x.Email == model.Email).Result.FirstOrDefault();
            if (findEmail != null)
            {
                return "Sorry, this e-mail allready exist!";
            }
            var res = await _unitOfWork.Users.AddAsync(_mapper.Map<UserDB>(model));
            var findUser = _unitOfWork.Users.FindAsync(x => x.AccountDB.Login == model.UserName).Result.FirstOrDefault();
            if (_bus.IsConnected==true)
            {
                await _bus.PublishAsync(new UserCreationMessage
                {
                    UserId = findUser.Id.ToString(),
                    FirstName = findUser.ProfileDB.FirstName,
                    LastName = findUser.ProfileDB.LastName,
                    UserName = findUser.AccountDB.Login,
                    Email = findUser.AccountDB.Email,
                });
                await _bus.PublishAsync(new AccountMessage
                {
                    UserId = findUser.Id,
                    UserName = findUser.AccountDB.Login,
                    Email = findUser.AccountDB.Email,
                    Type = AccountActionType.Registration,
                });
                await _bus.PublishAsync(new AccountLogMessage
                {
                    UserId = findUser.Id,
                    UserName = findUser.AccountDB.Login,
                    Email = findUser.AccountDB.Email,
                    Type = AccountActionType.Registration,
                });
                await _bus.PublishAsync(new RegistrationMessage
                {
                    Name = findUser.AccountDB.Login,
                    Email = findUser.AccountDB.Email,
                });
            }
            return "Ok";
        }

        public async Task<IEnumerable<ForViewModel>> GetAllAsync()
        {
            var res = await _unitOfWork.Users.GetAllAsync();
            var temp = _mapper.Map<IEnumerable<ForViewModel>>(res);
            return temp;
        }
        public IEnumerable<ForViewModel> GetAll()
        {
            var res = _unitOfWork.Users.GetAll();
            var temp = _mapper.Map<IEnumerable<ForViewModel>>(res);
            return temp;
        }

        public async Task<ModelWithHashPassw> GetByLogin(string login)
        {
            var model = await _unitOfWork.Users.GetByLoginAsync(login);
            return model != null ? (_mapper.Map<ModelWithHashPassw>(model)):(null);
        }

        public async Task<ForViewModel> Get(string id)
        {
            var model = await _unitOfWork.Users.GetAsync(id);
            return model != null ? (_mapper.Map<ForViewModel>(model)) : (null);
        }

        public async Task<string> Update(string id, ForUpdateModel userModel)
        {
            var findUser = _unitOfWork.Users.FindAsync(x => x.Id == id).Result.FirstOrDefault().ProfileDB;
            if (findUser == null) return "Not Found";
            var profileDB = _mapper.Map<ForUpdateModel, ProfileDB>(userModel);
            await _unitOfWork.Profiles.UpdateProfileAsync(profileDB, findUser.Id);
            return "Ok";
        }

        public async Task<string> UpdateByLogin(string login, ForUpdateModel userModel)
        {
            var findUser = _unitOfWork.Users.FindAsync(x => x.AccountDB.Login == login).Result.FirstOrDefault().ProfileDB;
            if (findUser == null) return "Not Found";
            var profileDB = _mapper.Map<ForUpdateModel, ProfileDB>(userModel);
            await _unitOfWork.Profiles.UpdateProfileAsync(profileDB, findUser.Id);
            return "Ok";
        }

        public async Task<string> Delete(string id)
        {
            var findUser = _unitOfWork.Users.FindAsync(x => x.Id == id).Result.FirstOrDefault();
            if (findUser == null) return "Not Found";
            await _unitOfWork.Users.DeleteAsync(findUser);
            if (_bus.IsConnected == true)
            {
                await _bus.PublishAsync(new AccountMessage
                {
                    UserId = findUser.Id,
                    UserName = findUser.AccountDB.Login,
                    Email = findUser.AccountDB.Email,
                    Type = AccountActionType.Delete,
                });
                await _bus.PublishAsync(new UserDeletedMessage
                {
                    UserId = findUser.Id
                });
            }
            return "Ok";
        }

        public async Task<bool> UpdateEmail(EmailUpdateMessage emailUpdateMessage)
        {
            var findUser = _unitOfWork.Users.FindAsync(x => x.Id == emailUpdateMessage.UserId).Result.FirstOrDefault();
            if (findUser == null) return false;
            findUser.AccountDB.Email = emailUpdateMessage.Email;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmailStatus(string login)
        {
            var findUser = _unitOfWork.Users.FindAsync(x => x.AccountDB.Login == login).Result.FirstOrDefault();
            if (findUser == null) return false;
            findUser.EmailConfirmed = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> GetEmail(string email)
        {
            var findEmail = await _unitOfWork.Accounts.FindAsync(x => x.Email == email);
            return findEmail.FirstOrDefault() != null ? true : false;
        }

        public Task<IEnumerable<GroupModel>> GetAllGroups()
        {
            throw new NotImplementedException();
        }

        public Task<string> AddGroup(GroupModel userGroupModel)
        {
            throw new NotImplementedException();
        }

        public Task<GroupModel> GetGroupById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GroupModel> UpdateGroupById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteGroupById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ForViewModel>> GetAllUsersInGroupById(int id)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
