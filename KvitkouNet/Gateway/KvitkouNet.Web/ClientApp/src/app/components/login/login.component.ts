import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { authPasswordFlowConfig } from 'src/app/auth.config';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userName: string;
  password: string;
  loginFailed = false;
  userProfile: object;

  constructor(private oauthService: OAuthService) {
    this.oauthService.configure(authPasswordFlowConfig);
  }

  ngOnInit() {
  }
  loadUserProfile(): void {
    this.oauthService.loadUserProfile().then(up => (this.userProfile = up));
  }

  get access_token() {
    return this.oauthService.getAccessToken();
  }

  get access_token_expiration() {
    return this.oauthService.getAccessTokenExpiration();
  }

  get givenName() {
    const claims = this.oauthService.getIdentityClaims();
    if (!claims) { return null; }
    return claims['given_name'];
  }

  get familyName() {
    const claims = this.oauthService.getIdentityClaims();
    if (!claims) { return null; }
    return claims['family_name'];
  }

  loginWithPassword() {
    this.oauthService
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(
        this.userName,
        this.password
      )
      .then(() => {
        console.log('successfully logged in');
        this.loginFailed = false;
      })
      .catch(err => {
        console.error('error logging in', err);
        this.loginFailed = true;
      });
  }

  logout() {
    this.oauthService.logOut(true);
  }
}
