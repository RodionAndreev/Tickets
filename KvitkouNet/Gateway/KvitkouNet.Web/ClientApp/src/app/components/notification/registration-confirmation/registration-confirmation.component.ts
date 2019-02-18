import { ActivatedRoute } from '@angular/router';
import { EmailNotificationService } from './../../../services/notification/emailNotification.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration-confirmation',
  templateUrl: './registration-confirmation.component.html',
  styleUrls: ['./registration-confirmation.component.css']
})
export class RegistrationConfirmationComponent implements OnInit {

  constructor(private service: EmailNotificationService, private activateRoute: ActivatedRoute) {
    let userName: string;
    activateRoute.params.subscribe(param =>  userName = param['username']);
    this.service.emailNotificationConfirmRegistration(userName).subscribe();
  }

  ngOnInit() {
  }

}
