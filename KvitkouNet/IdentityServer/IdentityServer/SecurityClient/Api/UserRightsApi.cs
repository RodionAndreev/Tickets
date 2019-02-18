using System;
using System.Collections.Generic;
using IdentityServer.SecurityClient.Client;
using IdentityServer.SecurityClient.Model;
using RestSharp;

namespace IdentityServer.SecurityClient.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IUserRightsApi
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="accessRequest"></param>
        /// <returns>AccessResponse</returns>
        AccessResponse UserRightsCheckUserRights (CheckAccessRequest accessRequest);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="request"></param>
        /// <returns>ActionResponse</returns>
        ActionResponse UserRightsEditUserRights (EditUserRightsRequest request);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserRightsResponse</returns>
        UserRightsResponse UserRightsGetUserRights (string id);
        /// <summary>
        ///  
        /// </summary>
        /// <param name="perPage"></param>
        /// <param name="page"></param>
        /// <param name="mask"></param>
        /// <returns>UserInfoResponse</returns>
        UserInfoResponse UserRightsGetUsers (int? perPage, int? page, string mask);
    }
  
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class UserRightsApi : IUserRightsApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRightsApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public UserRightsApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRightsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public UserRightsApi(String basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }
    
        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(String basePath)
        {
            this.ApiClient.BasePath = basePath;
        }
    
        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public String GetBasePath(String basePath)
        {
            return this.ApiClient.BasePath;
        }
    
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient {get; set;}
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="accessRequest"></param> 
        /// <returns>AccessResponse</returns>            
        public AccessResponse UserRightsCheckUserRights (CheckAccessRequest accessRequest)
        {
            
            // verify the required parameter 'accessRequest' is set
            if (accessRequest == null) throw new ApiException(400, "Missing required parameter 'accessRequest' when calling UserRightsCheckUserRights");
            
    
            var path = "/api/security/rights/check";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(accessRequest); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UserRightsCheckUserRights: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UserRightsCheckUserRights: " + response.ErrorMessage, response.ErrorMessage);
    
            return (AccessResponse) ApiClient.Deserialize(response.Content, typeof(AccessResponse), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="request"></param> 
        /// <returns>ActionResponse</returns>            
        public ActionResponse UserRightsEditUserRights (EditUserRightsRequest request)
        {
            
            // verify the required parameter 'request' is set
            if (request == null) throw new ApiException(400, "Missing required parameter 'request' when calling UserRightsEditUserRights");
            
    
            var path = "/api/security/rights/user";
            path = path.Replace("{format}", "json");
                
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                postBody = ApiClient.Serialize(request); // http body (model) parameter
    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.PUT, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UserRightsEditUserRights: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UserRightsEditUserRights: " + response.ErrorMessage, response.ErrorMessage);
    
            return (ActionResponse) ApiClient.Deserialize(response.Content, typeof(ActionResponse), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param> 
        /// <returns>UserRightsResponse</returns>            
        public UserRightsResponse UserRightsGetUserRights (string id)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling UserRightsGetUserRights");
            
    
            var path = "/api/security/rights/user/{id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
                                                    
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UserRightsGetUserRights: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UserRightsGetUserRights: " + response.ErrorMessage, response.ErrorMessage);
    
            return (UserRightsResponse) ApiClient.Deserialize(response.Content, typeof(UserRightsResponse), response.Headers);
        }
    
        /// <summary>
        ///  
        /// </summary>
        /// <param name="perPage"></param> 
        /// <param name="page"></param> 
        /// <param name="mask"></param> 
        /// <returns>UserInfoResponse</returns>            
        public UserInfoResponse UserRightsGetUsers (int? perPage, int? page, string mask)
        {
            
            // verify the required parameter 'perPage' is set
            if (perPage == null) throw new ApiException(400, "Missing required parameter 'perPage' when calling UserRightsGetUsers");
            
            // verify the required parameter 'page' is set
            if (page == null) throw new ApiException(400, "Missing required parameter 'page' when calling UserRightsGetUsers");
            
    
            var path = "/api/security/users/{per_page}/{page}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "per_page" + "}", ApiClient.ParameterToString(perPage));
path = path.Replace("{" + "page" + "}", ApiClient.ParameterToString(page));
    
            var queryParams = new Dictionary<String, String>();
            var headerParams = new Dictionary<String, String>();
            var formParams = new Dictionary<String, String>();
            var fileParams = new Dictionary<String, FileParameter>();
            String postBody = null;
    
             if (mask != null) queryParams.Add("mask", ApiClient.ParameterToString(mask)); // query parameter
                                        
            // authentication setting, if any
            String[] authSettings = new String[] {  };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UserRightsGetUsers: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UserRightsGetUsers: " + response.ErrorMessage, response.ErrorMessage);
    
            return (UserInfoResponse) ApiClient.Deserialize(response.Content, typeof(UserInfoResponse), response.Headers);
        }
    
    }
}
