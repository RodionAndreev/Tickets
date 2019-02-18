import { UserNotification } from './../../../models/notification/userNotification';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { NotificationService } from 'src/app/services/notification';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-notification-item',
  templateUrl: './notification-item.component.html',
  styleUrls: ['./notification-item.component.css']
})
export class NotificationItemComponent implements OnInit {

  public userNotifications: Array<UserNotification> = [];

  private userid: string;

  constructor(private service: NotificationService, private oauthService: OAuthService) {
    const authenticated = this.isAuthenticated();
    this.userid = authenticated ? this.getUserIdFromClaims() : 'BE86359-073C-434B-AD2D-A3932222DABE';
    service.notificationGetUserNotifications(this.userid).subscribe(data => this.userNotifications = data);
   }

  closeNotification(id: string) {
    this.service.notificationSetStatusClosed(id)
      .subscribe({complete: () => this.userNotifications = this.userNotifications.filter(x => x.notificationId !== id)});
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
