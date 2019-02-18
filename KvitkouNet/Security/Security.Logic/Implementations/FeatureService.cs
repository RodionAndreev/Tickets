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
    public class FeatureService : IFeatureService
    {
        private ISecurityData _securityContext;
        private IMapper _mapper;

        public FeatureService(ISecurityData securityContext, IMapper mapper)
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

        ~FeatureService()
        {
            Dispose(false);
        }

        #endregion

        public async Task<FeatureResponse> GetFeatures(int itemsPerPage, int pageNumber, string mask = null)
        {
            try
            {
                if (itemsPerPage < 1 || pageNumber < 1 || int.MaxValue / itemsPerPage < pageNumber || mask?.Trim().Length > 100)
                {
                    return new FeatureResponse
                    {
                        Message = "BadRequest",
                        Status = ActionStatus.Warning
                    };
                }

                var features = await _securityContext.GetFeatures(itemsPerPage, pageNumber, mask?.Trim() ?? "");

                return new FeatureResponse
                {
                    Features = _mapper.Map<Feature[]>(features.Features),
                    TotalCount = features.TotalCount,
                    Status = ActionStatus.Success
                };

            }
            catch (SecurityDbException e)
            {
                return new FeatureResponse
                {
                    Status = ActionStatus.Warning,
                    Message = PrettyExceptionHelper.GetMessage(e)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new FeatureResponse
                {
                    Status = ActionStatus.Error,
                    Message = "Something went wrong!"
                };
            }
        }

        public async Task<ActionResponse> AddFeature(string featureName)
        {
            try
            {
                if (string.IsNullOrEmpty(featureName) || featureName.Trim().Length > 100)
                {
                    return new AccessRightResponse
                    {
                        Message = "Name must be between 1 and 100 characters",
                        Status = ActionStatus.Warning
                    };
                }

                var id = await _securityContext.AddFeature(featureName);

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

        public async Task<ActionResponse> DeleteFeature(int featureId)
        {
            try
            {
                if (featureId == 0)
                {
                    return new ActionResponse
                    {
                        Message = "Nothing was deleted on id = 0",
                        Status = ActionStatus.Warning
                    };
                }
                await _securityContext.DeleteFeature(featureId);

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
        
        public async Task<ActionResponse> EditFeatureRights(int featureId, int[] featureRights)
        {
            try
            {
                featureRights = featureRights ?? new int[0];
                
                await _securityContext.EditFeatureRights(featureId,
                    featureRights.Select(l => l).ToArray());

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