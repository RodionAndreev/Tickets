import { environment } from './../../environments/environment';
import { SearchUserInfo } from './../models/searchUserInfo';
import { SearchTicketInfo } from './../models/searchTicketInfo';
import { SearchResult } from './../models/searchResult';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SearchTicket } from '../models/searchTicket';
import { SearchUser } from '../models/searchUser';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  constructor(private http: HttpClient, private oauthService: OAuthService) {}

  getTickets(request: SearchTicket) {
    return this.http.get<SearchResult<SearchTicketInfo>>(
      `${environment.baseUrl}/search/tickets?${this.toQueryString(
        request
      )}`,
      { headers: this.getHeaders() }
    );
  }

  getUsers(request: SearchUser) {
    return this.http.get<SearchResult<SearchUserInfo>>(
      `${environment.baseUrl}/search/users?${this.toQueryString(
        request
      )}`,
      { headers: this.getHeaders() }
    );
  }

  getPreviousTicketSearch() {
    return this.http.get<SearchTicket>(
      `${environment.baseUrl}/history/tickets`,
      { headers: this.getHeaders() }
    );
  }

  getPreviousUserSearch() {
    return this.http.get<SearchUser>(
      `${environment.baseUrl}/history/users`,
      { headers: this.getHeaders() }
    );
  }

  isAuthenticated() {
    const token = this.oauthService.getAccessToken();
    return !!token;
  }

  private toQueryString(obj) {
    return Object.keys(obj)
      .map(k => `${encodeURIComponent(k)}=${encodeURIComponent(obj[k])}`)
      .join('&');
  }

  private getHeaders() {
    const token = this.oauthService.getAccessToken();
    return !!token
      ? new HttpHeaders({
          Authorization: 'Bearer ' + token
        })
      : new HttpHeaders();
  }
}
