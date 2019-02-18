import { Component, OnInit } from '@angular/core';
import { LogService } from '../../../services/log.service';
import { DealLogEntry } from '../../../models/dealLogEntry';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-deal-logs',
  templateUrl: './deal-logs.component.html',
  styleUrls: ['./deal-logs.component.css']
})
export class DealLogsComponent implements OnInit {
  dealLogs: DealLogEntry[];
  dealLogTableHeaders = ['Id', 'Дата', 'Id владельца', 'Id получателя', 'Цена', 'Тип'];
  dealLogsFormGroup: FormGroup;
  constructor(private logService: LogService) {}

  ngOnInit() {
    this.dealLogsFormGroup = new FormGroup({
      dateFrom: new FormControl(),
      dateTo: new FormControl(),
      ticketId: new FormControl(''),
      ownerId: new FormControl(''),
      recieverId: new FormControl(''),
      minPrice: new FormControl(),
      maxPrice: new FormControl(),
      type: new FormControl()
    })
  }

  onSubmit() {
    this.logService.getDealLogs(this.getParamsString())
      .subscribe(result => this.dealLogs = result, err => console.error(err))
  }

  getParamsString()  {
    const params = new URLSearchParams();
    const formValue = this.dealLogsFormGroup.value;

    for (const key in formValue) {
      params.append(key, formValue[key]);
    }
     return params.toString();
  }

}
