import { Component, OnInit } from '@angular/core';
import { EmailNotificationService, EmailNotification } from 'src/app/services/notification';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-email-notification-item',
  templateUrl: './email-notification-item.component.html',
  styleUrls: ['./email-notification-item.component.css']
})
export class EmailNotificationItemComponent implements OnInit {

  public emailNotifications: Array<EmailNotification> = [];

  private userid: string;

  constructor(private service: EmailNotificationService, private oauthService: OAuthService) {
    const authenticated = this.isAuthenticated();
    this.userid = authenticated ? this.getUserIdFromClaims() : 'BE86359-073C-434B-AD2D-A3932222DABE';
    service.emailNotificationGetEmailNotifications(this.userid)
      .subscribe(data => this.emailNotifications = data);
   }

   closeNotification(id: string) {
       this.emailNotifications = this.emailNotifications.filter(x => x.notificationId !== id);
  }

  ngOnInit() {
  }

  private getUserIdFromClaims() {
    const claims = this.oauthService.getIdentityClaims();
    if (claims) {
      return claims['sub'];
    }
  }

  private isAuthenticated() {
    const token = this.oauthService.getAccessToken();
    return !! token ? true : false;
  }
}
