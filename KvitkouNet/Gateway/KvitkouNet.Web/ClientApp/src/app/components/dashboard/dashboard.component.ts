import { DashboardService } from '../../services/dashboard//dashboard.service';
import {News} from '../../models/dashboard/models';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  news: News[] = [];

  constructor(private newsSrv: DashboardService) { }

  ngOnInit() {
    this.newsSrv.getNews().subscribe((result) => this.news = result,
    err => console.error(err));
   }

}

