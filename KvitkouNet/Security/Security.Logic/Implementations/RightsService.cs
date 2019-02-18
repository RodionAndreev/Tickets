using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Security.Data;
using Security.Data.Exceptions;
using Security.Logic.Helpers;
using Security.Logic.Models;
using Security.Logic.Models.Enums;
using Security.Logic.Models.Responses;
using Security.Logic.Services;

namespace Security.Logic.Implementations
{
    public class RightsService : IRightsService
    {
        private ISecurityData _securityContext;
        private IMapper _mapper;

        public RightsService(ISecurityData securityContext, IMapper mapper)
        {
            _securityContext = securityContext;
            _mapper = mapper;
        }

        #region DisposeImp
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            _securityContext.Dispose();
        }

        ~RightsService()
        {
            Dispose(false);
        }

        #endregion

        public async Task<AccessRightResponse> GetRights(int itemsPerPage, int pageNumber, string mask = null)
        {
            try
            {
                if (itemsPerPage < 1 || pageNumber < 1 || int.MaxValue / itemsPerPage < pageNumber || mask?.Trim().Length > 100)
                {
                    return new AccessRightResponse
                    {
                        Status = ActionStatus.Warning,
                        Message = "BadRequest"
                    };
                }

                var rights = await
                    _securityContext.GetRights(itemsPerPage, pageNumber, mask?.Trim() ?? "");

                return new AccessRightResponse
                {
                    AccessRights = _mapper.Map<AccessRight[]>(rights.Rights),
                    TotalCount = rights.TotalCount,
                    Status = ActionStatus.Success
                };
            }
            catch (SecurityDbException e)
            {
                return new AccessRightResponse
                {
                    Status = ActionStatus.Warning,
                    Message = PrettyExceptionHelper.GetMessage(e)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new AccessRightResponse
                {
                    Status = ActionStatus.Error,
                    Message = "Something went wrong!"
                };
            }
        }

        public async Task<AccessRightResponse> AddRights(string[] rights)
        {
            try
            {
                if (rights.Any(l=>string.IsNullOrEmpty(l) || l.Trim().Length > 100))
                {
                    return new AccessRightResponse
                    {
                        Message = "Name must be between 1 and 100 characters",
                        Status = ActionStatus.Warning
                    };
                }

                return new AccessRightResponse
                {
                    AccessRights = _mapper.Map<IEnumerable<AccessRight>>(
                        await _securityContext.AddRights(rights)).ToArray(),
                    Status = ActionStatus.Success
                };

            }
            catch (SecurityDbException e)
            {
                return new AccessRightResponse
                {
                    Status = ActionStatus.Warning,
                    Message = PrettyExceptionHelper.GetMessage(e)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new AccessRightResponse
                {
                    Status = ActionStatus.Error,
                    Message = "Something went wrong!"
                };
            }
        }

        public async Task<ActionResponse> DeleteRight(int rightId)
        {
            try
            {
                if (rightId == 0)
                {
                   return new ActionResponse
                    {
                        Message = "Nothing was deleted on id = 0",
                        Status = ActionStatus.Warning
                    };
                }
                await _securityContext.DeleteRight(rightId);
                
                return new ActionResponse
                {
                    Status = ActionStatus.Success
                };

            }
            catch (SecurityDbException e)
            {
                return new ActionResponse
                {
                    Status = ActionStatus.Warning,
                    Message = PrettyExceptionHelper.GetMessage(e)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ActionResponse
                {
                    Status = ActionStatus.Error,
                    Message = "Something went wrong!"
                };
            }
        }
    }
}