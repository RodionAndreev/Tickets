import { RoleService } from 'src/app/services/security/role.service';
import { AccessRight } from './../../../models/security/accessRight';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-new-role',
  templateUrl: './new-role.component.html',
  styleUrls: ['./new-role.component.css']
})
export class NewRoleComponent implements OnInit {
  roleForm: FormGroup;
  secceded: boolean;
  errorMessage: string;
  constructor(private service: RoleService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.roleForm = this.formBuilder.group({
      roleName: ['']
  });
  }
  onAddRole() {
    this.service.roleAddRole('"' + this.roleForm.get('roleName').value + '"').subscribe(result => {
      this.secceded = result.status === 0;
      this.errorMessage = result.message; });
  }
  onRoleNameChanged() {
    this.secceded = false;
  }

}
