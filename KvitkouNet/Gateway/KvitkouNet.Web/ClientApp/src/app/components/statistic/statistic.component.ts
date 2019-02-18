import { Subscription } from 'rxjs';
import {
  StatisticService,
  StatisticOnline,
  RangeDate,
  Areas
} from './../../services/statistic.service';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.css']
})
export class StatisticComponent implements OnInit {
  userList: StatisticOnline[] = [];
  range: RangeDate = { startDate: new Date(), endDate: new Date() };
  areas: Areas[] = [];
  statisticIndex: number;
  gridDataSource: any;
  gridDataSource2: any;
  customersData: any;
  url: string;

  constructor(private statisticService: StatisticService) {
    this.statisticIndex = 0; // 0 - статистика посещений 1 - online и т.д.
    this.setDate(1);
    this.url = 'http://localhost:5001/statistic/user';

    this.gridDataSource = AspNetData.createStore({
      key: 'userId',
      loadUrl: this.url + '/all',
      onBeforeSend: function(method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: false };
      }
    });
  }

  setDate(year: number) {
    this.range.startDate.setFullYear(new Date().getFullYear() - year);
  }

  ConvertDate(item: StatisticOnline) {
    const year = new Date(item.createTime).getFullYear();
    const month = new Date(item.createTime).getMonth() + 1;

    item.createTime = year + '/' + month;
    return item;
  }

  convertAreas(model: StatisticOnline) {
    if (model) {
      const item: Areas[] = [];
      item.push({ legend: 'Гости', count: model.countGuest });
      item.push({ legend: 'Зарегистрированные', count: model.countRegistered });
      return item;
    }
    return [];
  }

  getStatistic() {
    this.statisticService
      .getRange(this.range)
      .subscribe(
        result => (this.userList = result.map(this.ConvertDate)),
        err => console.log(err)
      );
  }

  getOnlineAll() {
    this.statisticService
      .getAreas()
      .subscribe(
        result => (this.areas = this.convertAreas(result)),
        err => console.log(err)
      );
  }

  clickRange(year: number) {
    this.setDate(year);
    this.getStatistic();
  }

  clickButton(index: number) {
    this.statisticIndex = index;
  }

  ngOnInit() {
    this.getOnlineAll();
    this.getStatistic();
  }
}
