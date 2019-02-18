import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProfileModel } from '../../models/user-settings/update-fio';
import { Users } from 'src/app/models/users';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class UpdateFioService {
  private baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getProfile(id) {
    return this.http.get<Users>(`${this.baseUrl}/api/users/${id}`);
  }
  
  putProfile(id, body) {
    return this.http.put(`${this.baseUrl}/api/users/${id}`, body)
  }
}
