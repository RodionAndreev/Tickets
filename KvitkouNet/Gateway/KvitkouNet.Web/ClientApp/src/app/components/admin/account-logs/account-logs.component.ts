import { Component, OnInit } from '@angular/core';
import { LogService } from '../../../services/log.service';
import { AccountLogEntry } from '../../../models/accountLogEntry';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-account-logs',
  templateUrl: './account-logs.component.html',
  styleUrls: ['./account-logs.component.css']
})
export class AccountLogsComponent implements OnInit {
  accountLogs: AccountLogEntry[];
  accountLogTableHeaders = ['Id', 'Дата', 'Id пользователя', 'Имя', 'Email', 'Тип'];
  accountLogsFormGroup: FormGroup;
  constructor(private logService: LogService) { }

  ngOnInit() {
    this.accountLogsFormGroup = new FormGroup({
      dateFrom: new FormControl(),
      dateTo: new FormControl(),
      userId: new FormControl(''),
      userName: new FormControl(''),
      email: new FormControl(''),
      type: new FormControl()
    })
  }

  onSubmit() {
    this.logService.getAccountLogs(this.getParamsString())
      .subscribe(result => this.accountLogs = result, err => console.error(err))
  }

  getParamsString()  {
    const params = new URLSearchParams();
    const formValue = this.accountLogsFormGroup.value;

    for (const key in formValue) {
      params.append(key, formValue[key]);
    }
    return params.toString();
  }
}
