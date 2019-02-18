using System;
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
    public class RoleService : IRoleService
    {
        private ISecurityData _securityContext;
        private IMapper _mapper;

        public RoleService(ISecurityData securityContext, IMapper mapper)
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

        ~RoleService()
        {
            Dispose(false);
        }

        #endregion

        public async Task<RoleResponse> GetRoles(int itemsPerPage, int pageNumber, string mask = null)
        {
            try
            {
                if (itemsPerPage < 1 || pageNumber < 1 || int.MaxValue / itemsPerPage < pageNumber || mask?.Trim().Length > 100)
                {
                    return new RoleResponse
                    {
                        Status = ActionStatus.Warning,
                        Message = "BadRequest"
                    };
                }

                var roles = await _securityContext.GetRoles(itemsPerPage, pageNumber, mask?.Trim() ?? "");

                return new RoleResponse
                {
                    Roles = _mapper.Map<Role[]>(roles.Roles),
                    TotalCount = roles.TotalCount,
                    Status = ActionStatus.Success
                };
            }
            catch (SecurityDbException e)
            {
                return new RoleResponse
                {
                    Status = ActionStatus.Warning,
                    Message = PrettyExceptionHelper.GetMessage(e)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new RoleResponse
                {
                    Status = ActionStatus.Error,
                    Message = "Something went wrong!"
                };
            }
        }

        public async Task<ActionResponse> AddRole(string roleName)
        {
            try
            {
                if (string.IsNullOrEmpty(roleName) || roleName.Trim().Length > 100)
                {
                    return new AccessRightResponse
                    {
                        Message = "Name must be between 1 and 100 characters",
                        Status = ActionStatus.Warning
                    };
                }

                var id = await _securityContext.AddRole(roleName);

                return new ActionResponse
                {
                    Id = id,
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

        public async Task<ActionResponse> DeleteRole(int roleId)
        {
            try
            {
                if (roleId == 0)
                {
                    return new ActionResponse
                    {
                        Message = "Nothing was deleted on id = 0",
                        Status = ActionStatus.Warning
                    };
                }
                await _securityContext.DeleteRole(roleId);

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

        public async Task<ActionResponse> EditRole(int roleId, int[] accessRightsIds, int[] deniedRightsIds, int[] functionIds)
        {
            try
            {
                accessRightsIds = accessRightsIds ?? new int[0];
                deniedRightsIds = deniedRightsIds ?? new int[0];

                if (accessRightsIds.Intersect(deniedRightsIds).Any())
                {
                    return new ActionResponse
                    {
                        Status = ActionStatus.Warning,
                        Message = "Accessed and denied must not have same Rights"
                    };
                }

                await _securityContext.EditRoleFunctions(roleId, functionIds);
                await _securityContext.EditRoleRights(roleId, accessRightsIds, deniedRightsIds);

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