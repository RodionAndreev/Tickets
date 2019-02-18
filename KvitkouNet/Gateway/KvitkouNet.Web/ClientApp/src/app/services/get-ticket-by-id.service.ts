import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Ticket } from '../models/ticket';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class GetTicketByIdService {
  private baseUrl = 'https://localhost:5002';

  constructor(private http: HttpClient, private oauthService: OAuthService) {}

  getTicketById(id) {
    return this.http.get<Ticket>(`${this.baseUrl}/tickets/${id}`, {
      headers: this.getHeaders()
    });
  }
  delTicketById(id) {
    return this.http.delete(`${this.baseUrl}/tickets/${id}`, {
      headers: this.getHeaders()
    });
  }
    editTicketById(id) {
      return this.http.put(`${this.baseUrl}/tickets/${id}/add`, {
        headers: this.getHeaders()
      });
  }
  subsсribe(body, id: string) {
    return this.http.put(
      `${this.baseUrl}/tickets/${id}/add`,
      body,
      { headers: this.getHeaders() }
    );
  }
  isAuthenticated() {
    const token = this.oauthService.getAccessToken();
    return !!token ? true : false;
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
