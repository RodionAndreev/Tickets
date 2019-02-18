import { Component, OnInit } from '@angular/core';
import { LogService } from '../../../services/log.service';
import { FormGroup, FormControl } from '@angular/forms';
import { PaymentLogEntry } from '../../../models/paymentLogEntry';

@Component({
  selector: 'app-payment-logs',
  templateUrl: './payment-logs.component.html',
  styleUrls: ['./payment-logs.component.css']
})
export class PaymentLogsComponent implements OnInit {
  paymentLogs: PaymentLogEntry[];
  paymentLogTableHeaders = ['Id', 'Дата', 'Id отправителя', 'Id получателя', 'Сумма'];
  paymentLogsFormGroup: FormGroup;
  constructor(private logService: LogService) { }

  ngOnInit() {
    this.paymentLogsFormGroup = new FormGroup({
      dateFrom: new FormControl(),
      dateTo: new FormControl(),
      senderId: new FormControl(''),
      reciverId: new FormControl(''),
      minTransfer: new FormControl(),
      maxTransfer: new FormControl()
    })
  }

  onSubmit() {
    this.logService.getPaymentLogs(this.getParamsString())
      .subscribe(result => this.paymentLogs = result, err => console.error(err))
  }

  getParamsString()  {
    const params = new URLSearchParams();
    const formValue = this.paymentLogsFormGroup.value;

    for (const key in formValue) {
      params.append(key, formValue[key]);
    }
    console.log(params.toString());
    return params.toString();
  }

}
