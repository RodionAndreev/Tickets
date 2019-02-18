import { AuthConfig } from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {

  tokenEndpoint : 'https://localhost:5002/connect/token',

  userinfoEndpoint  : 'https://localhost:5002/connect/userinfo',

  strictDiscoveryDocumentValidation: false,

  requireHttps: false,

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin + '/index.html',

  // The SPA's id. The SPA is registerd with this id at the auth-server
  clientId: 'oauthClient',

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'openid api',
};

export const authPasswordFlowConfig: AuthConfig = {
  // Url of the Identity Provider
  tokenEndpoint : 'https://localhost:5002/connect/token',

  userinfoEndpoint  : 'https://localhost:5002/connect/userinfo',

  strictDiscoveryDocumentValidation: false,

  requireHttps: false,

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin + '/index.html',

  // URL of the SPA to redirect the user after silent refresh
  silentRefreshRedirectUri: window.location.origin + '/index.html',

  // The SPA's id. The SPA is registerd with this id at the auth-server
  clientId: 'oauthClient',

  dummyClientSecret: 'superSecretPassword',

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC. The 4th is a usecase-specific one
  scope: 'openid api',

  showDebugInformation: true,

  oidc: false
};
