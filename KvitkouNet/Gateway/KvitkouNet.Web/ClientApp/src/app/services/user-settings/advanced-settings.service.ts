import { UserSettings } from './../../models/user-settings/userSettings';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdvancedSettingsService {
  private baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }
  getSettings(id) {
    return this.http.get<UserSettings>(`${this.baseUrl}/api/settings/${id}`);
  }
  putSettings(id, body) {
    return this.http.put(`${this.baseUrl}/api/settings/${id}/update`, body)
  }
}
