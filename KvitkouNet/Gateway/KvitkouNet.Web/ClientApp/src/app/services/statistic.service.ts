import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export class CountryInfo {
  state: string;
  oil: number;
  gas?: number;
  coal?: number;
}

export interface StatisticOnline {
  id: number;
  countAll: number;
  countRegistered: number;
  countGuest: number;
  createTime: string;
}

export interface RangeDate {
  startDate: Date;
  endDate: Date;
}

export interface Areas {
  legend: string;
  count: number;
}

@Injectable()
export class StatisticService {
  constructor(private http: HttpClient) {}

  myMap(item: StatisticOnline) {
    const year = new Date(item.createTime).getFullYear();
    const month = new Date(item.createTime).getMonth();

    item.createTime = year + '-' + month;
    return item;
  }

  getRange(range: RangeDate) {
    return this.http.post<StatisticOnline[]>(
      'http://localhost:5001/statistics/count/range',
      range
    );
  }

  getAreas() {
    return this.http.get<StatisticOnline>(
      'http://localhost:5001/statistics/count/all'
    );
  }
}
