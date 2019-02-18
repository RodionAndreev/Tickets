import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  authenticated: boolean;

  constructor(private oauthService: OAuthService)
  {
    this.authenticated = this.isAuthenticated();
  }

  ngOnInit(): void {
    this.authenticated = this.isAuthenticated();
  }

 public isAuthenticated() {

    const token = this.oauthService.getAccessToken();
    return !! token ? true : false;
  }

  public logoff() {
      this.oauthService.logOut(true);
  }

  public get name() {
      const claims = this.oauthService.getIdentityClaims();
      if (!claims) { return null; }
      return claims['given_name'];
  }

  public isNeedToHideAdminPanel() : boolean{
    return !this.oauthService.hasValidAccessToken();
  }

}
