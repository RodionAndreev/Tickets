import { Component, OnInit } from '@angular/core';
import { ErrorLogEntry } from '../../../models/errorLogEntry';
import { LogService } from '../../../services/log.service';
import {
  FormControl,
  Validators,
  FormGroup,
  FormBuilder
} from '@angular/forms';

@Component({
  selector: 'app-error-logs',
  templateUrl: './error-logs.component.html',
  styleUrls: ['./error-logs.component.css']
})
export class ErrorLogsComponent implements OnInit {
  errorLogs: ErrorLogEntry[];
  errorLogTableHeaders = ['Id', 'Дата', 'Микросервис', 'Тип', 'Hresult', 'InnerEx', 'Сообщение', 'Источник', 'StackTrace', 'TargetSitename'];
  errorLogsFormGroup: FormGroup;


  constructor(
    private logService: LogService
  ) { }

  ngOnInit() {
    this.errorLogsFormGroup = new FormGroup({
      dateFrom: new FormControl(),
      dateTo: new FormControl(),
      serviceName: new FormControl(''),
      exceptionTypeName: new FormControl(''),
      message: new FormControl('')
    })
  }

  onSubmit() {
    this.logService.getErrorLogs(this.getParamsString())
      .subscribe(result => this.errorLogs = result, err => console.error(err))
  }

  getParamsString()  {
    const params = new URLSearchParams();
    const formValue = this.errorLogsFormGroup.value;

    for (const key in formValue) {
      params.append(key, formValue[key]);
    }
    console.log(params.toString());
    return params.toString();
  }
}