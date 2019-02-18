import { Component, OnInit } from '@angular/core';
import { Users } from 'src/app/models/users';
import { UsersService } from 'src/app/services/users/users.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: Users[] = [];
  constructor(
    private usersSrv: UsersService,
    private router: Router
    ) {}

  ngOnInit() {
    this.usersSrv.getUsers().subscribe(result => (this.users = result), err => console.error(err));
  }
}
