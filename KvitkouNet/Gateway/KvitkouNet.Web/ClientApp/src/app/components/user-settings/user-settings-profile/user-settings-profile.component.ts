import { OAuthService } from 'angular-oauth2-oidc';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Users } from 'src/app/models/users';
import { UpdateFioService } from 'src/app/services/user-settings/update-fio.service';
import { ForUpdateModel } from 'src/app/models/users/forUpdateModel';
import * as jwt_decode from "jwt-decode";

@Component({
  selector: 'app-user-settings-profile',
  templateUrl: './user-settings-profile.component.html',
  styleUrls: ['./user-settings-profile.component.css']
})
export class UserSettingsProfileComponent implements OnInit {
  user: Users
  model: ForUpdateModel
  id: string
  response: boolean = true
  userSettingsProfile = new FormGroup({
    first: new FormControl(''),
    middle: new FormControl(''),
    last: new FormControl('')
  });
  constructor(private updateFioService: UpdateFioService, private oauthService: OAuthService) { 
    this.id = this.getUserId();
    if(this.user == null)
    {
      this.user = new Users()
      this.user.firstName = ""
      this.user.lastName = ""
    }
    if(this.model == null)
    {
      this.model = new ForUpdateModel()
    }
  }

  ngOnInit() {
    console.log(this.response)
    this.updateFioService.getProfile(this.id).subscribe(result=>(this.user = result), err => console.log(err));
  }
  onPut(){
    this.response = false
    console.log(this.response)
    this.model.firstName = this.user.firstName;
    this.model.lastName = this.user.lastName;
    this.model.birthday = this.user.birthday;
    this.updateFioService.putProfile(this.id, this.model).subscribe((data: boolean)=>this.response = data);
    
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
