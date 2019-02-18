import { News } from './../models/dashboard/news';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot
} from '@angular/router';

@Injectable({ providedIn: 'root' })

export class DashboardService {
   private baseUrl = 'http://localhost:5009;';

  constructor(private http: HttpClient) {}

  getNews() {
    return this.http.get<News[]>(`${this.baseUrl}/api/news`);
  }
}
