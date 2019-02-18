import { UserInfo } from './../../../models/security/userInfo';
import { UserRightsService } from 'src/app/services/security/userRights.service';
import { UsersService } from '../../../services/users/users.service';
import { Component, OnInit } from '@angular/core';
import { FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-security-menu',
  templateUrl: './security-menu.component.html',
  styleUrls: ['./security-menu.component.css']
})
export class SecurityMenuComponent implements OnInit {
  userSearchForm: FormGroup;
  errorMessage: string;
  usersFound: UserInfo[];
  constructor(private userService: UserRightsService, private formBuilder: FormBuilder) {

   }

  ngOnInit() {
    this.userSearchForm = this.formBuilder.group({
      userSearch: ['']
    });
  }

  onSearchUser() {
    this.userService.userRightsGetUsers(10, 1, this.userSearchForm.get('userSearch').value).subscribe(users => {
      this.errorMessage = users.message;
      this.usersFound = users.usersInfo; });
  }
  onUserSelected(user: UserInfo) {

  }

}
