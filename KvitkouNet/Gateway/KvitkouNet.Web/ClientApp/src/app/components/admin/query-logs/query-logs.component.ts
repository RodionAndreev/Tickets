import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { QueryLogEntry } from '../../../models/queryLogEntry';
import { LogService } from '../../../services/log.service';

@Component({
  selector: 'app-query-logs',
  templateUrl: './query-logs.component.html',
  styleUrls: ['./query-logs.component.css']
})
export class QueryLogsComponent implements OnInit {
  queryLogs: QueryLogEntry[];
  queryLogTableHeaders = ['Id', 'Дата', 'UserId', 'Поисковый критерий', 'Фильтры'];
  queryLogsFormGroup: FormGroup;
  constructor(private logService: LogService) { }

  ngOnInit() {
    this.queryLogsFormGroup = new FormGroup({
      dateFrom: new FormControl(),
      dateTo: new FormControl(),
      userId: new FormControl(''),
      searchCriterium: new FormControl('')
    })
  }

  onSubmit() {
    this.logService.getQueryLogs(this.getParamsString())
      .subscribe(result => this.queryLogs = result, err => console.error(err))
  }

  getParamsString()  {
    const params = new URLSearchParams();
    const formValue = this.queryLogsFormGroup.value;

    for (const key in formValue) {
      params.append(key, formValue[key]);
    }
     return params.toString();
  }

}
