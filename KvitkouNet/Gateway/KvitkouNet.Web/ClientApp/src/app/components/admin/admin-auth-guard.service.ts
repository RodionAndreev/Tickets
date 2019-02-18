import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, CanActivateChild } from '@angular/router';
import { OAuthService } from 'angular-oauth2-oidc';
import * as jwt_decode from "jwt-decode";

@Injectable()
export class AdminAuthGuard implements CanActivate, CanActivateChild {

  constructor(private oauthService: OAuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    var decodedToken = this.getDecodedAccessToken(this.oauthService.getAccessToken());
    if (decodedToken['role'] == 'admin') {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    var decodedToken = this.getDecodedAccessToken(this.oauthService.getAccessToken());
    if (decodedToken['role'] == 'admin') {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }

  getDecodedAccessToken(token: string): any {
    try{
        return jwt_decode(token);
    }
    catch(Error){
        return null;
    }
  }
}