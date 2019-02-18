import { Subscription } from './../../../models/notification/subscription';
import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from 'src/app/services/notification';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-subscription-item',
  templateUrl: './subscription-item.component.html',
  styleUrls: ['./subscription-item.component.css']
})
export class SubscriptionItemComponent implements OnInit {

  public subscriptions: Array<Subscription> = [];

  private userid: string;

  constructor(private service: SubscriptionService, private oauthService: OAuthService) {
    const authenticated = this.isAuthenticated();
    this.userid = authenticated ? this.getUserIdFromClaims() : 'BE86359-073C-434B-AD2D-A3932222DABE';
    service.subscriptionGetAll(this.userid)
      .subscribe(data => this.subscriptions = data);
   }

   closeSubscription(theme: string) {
    this.service.subscriptionUnsubscribe(this.userid, theme)
      .subscribe({complete: () => this.subscriptions = this.subscriptions.filter(x => x.theme !== theme)});
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
