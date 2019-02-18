import { Component, OnInit } from '@angular/core';
import { LogService } from '../../../services/log.service';
import { TicketLogEntry } from '../../../models/ticketLogEntry';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-ticket-logs',
  templateUrl: './ticket-logs.component.html',
  styleUrls: ['./ticket-logs.component.css']
})
export class TicketLogsComponent implements OnInit {
  ticketLogs: TicketLogEntry[];
  ticketLogTableHeaders = ['Id', 'Дата', 'UserId', 'Ticketid', 'Имя билета', 'Тип', 'Описание'];
  ticketLogsFormGroup: FormGroup;
  constructor(private logService: LogService) { }

  ngOnInit() {
    this.ticketLogsFormGroup = new FormGroup({
      dateFrom: new FormControl(),
      dateTo: new FormControl(),
      ticketId: new FormControl(''),
      ticketName: new FormControl(''),
      actionType: new FormControl(),
      description: new FormControl('')
    })
  }
  onSubmit() {
    this.logService.getTicketLogs(this.getParamsString())
      .subscribe(result => this.ticketLogs = result, err => console.error(err))
  }

  getParamsString()  {
    const params = new URLSearchParams();
    const formValue = this.ticketLogsFormGroup.value;

    for (const key in formValue) {
      params.append(key, formValue[key]);
    }
     return params.toString();
  }
}
