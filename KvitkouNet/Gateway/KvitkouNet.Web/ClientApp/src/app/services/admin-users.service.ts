import { OAuthService } from 'angular-oauth2-oidc';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Users } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class AdminUsersService {
  private baseUrl = 'https://localhost:5002';
  constructor(private http: HttpClient, private oauthService: OAuthService) { }

  getUsers() {
    var headers = this.getHttpHeaders();
    return this.http.get<Users[]>(`${this.baseUrl}/admin/users`, {headers});
  }

  private getHttpHeaders(): HttpHeaders {
    var token = this.oauthService.getAccessToken();
    if (token != null) {
      return new HttpHeaders({
        "Authorization": "Bearer " + this.oauthService.getAccessToken()
      });
    }
    else {
      return new HttpHeaders();
    }
  }
}
