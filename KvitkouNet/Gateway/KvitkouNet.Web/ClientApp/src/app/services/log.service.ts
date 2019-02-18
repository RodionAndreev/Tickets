import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorLogEntry } from '../models/errorLogEntry';
import { AccountLogEntry } from '../models/accountLogEntry';
import { PaymentLogEntry } from '../models/paymentLogEntry';
import { QueryLogEntry } from '../models/queryLogEntry';
import { TicketLogEntry } from '../models/ticketLogEntry';
import { DealLogEntry } from '../models/dealLogEntry';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  private baseUrl = 'https://localhost:5002/';

  constructor(private http: HttpClient, private oauthService: OAuthService) {
  }

  getErrorLogs(queryParams: string) {
    var headers = this.getHttpHeaders();
    return this.http.get<ErrorLogEntry[]>(`${this.baseUrl}admin/logs/errors?${queryParams}`, { headers });
  }

  getAccountLogs(queryParams: string) {
    var headers = this.getHttpHeaders();
    return this.http.get<AccountLogEntry[]>(`${this.baseUrl}admin/logs/accounts?${queryParams}`, { headers });
  }

  getPaymentLogs(queryParams: string) {
    var headers = this.getHttpHeaders();
    return this.http.get<PaymentLogEntry[]>(`${this.baseUrl}admin/logs/payments?${queryParams}`, { headers });
  }

  getQueryLogs(queryParams: string) {
    var headers = this.getHttpHeaders();
    return this.http.get<QueryLogEntry[]>(`${this.baseUrl}admin/logs/queries?${queryParams}`, { headers });
  }

  getTicketLogs(queryParams: string) {
    var headers = this.getHttpHeaders();
    return this.http.get<TicketLogEntry[]>(`${this.baseUrl}admin/logs/tickets?${queryParams}`, { headers });
  }

  getDealLogs(queryParams: string) {
    var headers = this.getHttpHeaders();
    return this.http.get<DealLogEntry[]>(`${this.baseUrl}admin/logs/deals?${queryParams}`, { headers });
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
