import { Component, OnInit } from '@angular/core';
import { Users } from 'src/app/models/users';
import { AdminUsersService } from '../../../services/admin-users.service';

@Component({
  selector: 'app-admin-user',
  templateUrl: './admin-user.component.html',
  styleUrls: ['./admin-user.component.css']
})
export class AdminUserComponent implements OnInit {
  users: Users[] = [];

  constructor(private usersSrv: AdminUsersService) { }

  ngOnInit() {
    this.usersSrv.getUsers().subscribe(result => (this.users = result), err => console.error(err));
  }

}
