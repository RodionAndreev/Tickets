import { Component, OnInit } from '@angular/core';
import { UserSettings } from 'src/app/models/user-settings/userSettings';
import { AdvancedSettingsService } from 'src/app/services/user-settings/advanced-settings.service';
import { OAuthService } from 'angular-oauth2-oidc';
import * as jwt_decode from "jwt-decode";

@Component({
  selector: 'app-user-settings-advanced',
  templateUrl: './user-settings-advanced.component.html',
  styleUrls: ['./user-settings-advanced.component.css']
})
export class UserSettingsAdvancedComponent implements OnInit {
  userSettingsModel: UserSettings
  id: string
  constructor(private advansedService: AdvancedSettingsService, private oauthService: OAuthService) { 
    this.id = this.getUserId();
    if(this.userSettingsModel == null)
    {
      this.userSettingsModel = new UserSettings()
      this.userSettingsModel.preferAddress = ""
    }
  }

  ngOnInit() {
    this.advansedService.getSettings(this.id).subscribe(result=>(this.userSettingsModel = result), err => console.log(err));
  }
  onPut() {
    console.log(this.userSettingsModel.preferAddress)
    this.advansedService.putSettings(this.id, this.userSettingsModel).subscribe(err => console.log(err));
  }
  onPrivateChanged(value:boolean){
    this.userSettingsModel.isPrivateAccount = value;
  }
  onGetInfoChanged(value:boolean){
    this.userSettingsModel.isGetTicketInfo = value;
  }
  getUserId(): string {
    var decodedToken = this.getDecodedAccessToken(this.oauthService.getAccessToken());
    var result = decodedToken == null ? 11 : decodedToken['id'];
    return result;

    }
  getDecodedAccessToken(token: string): any {
     try{
        return jwt_decode(token);
    }
     catch(Error){
         return null;
     }
  }
}
