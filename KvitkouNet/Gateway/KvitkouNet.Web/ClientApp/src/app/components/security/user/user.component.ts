import { EditUserRightsRequest } from './../../../models/security/editUserRightsRequest';
import { UserRights } from './../../../models/security/userRights';
import { UserRightsService } from './../../../services/security/userRights.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FunctionsService, RightsService, RoleService } from 'src/app/services/security/api';
import { Role } from 'src/app/models/security/role';
import { AccessFunction, AccessRight } from 'src/app/models/security/models';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  userForm: FormGroup;
  user: UserRights;
  errorMessage: string;

  selectedRole: Role;
  selectedFunction: AccessFunction;
  selectedAccessRight: AccessRight;
  selectedDeniedRight: AccessRight;

  rolesFound: Role[];
  functionsFound: AccessFunction[];
  accessRightsFound: AccessRight[];
  deniedRightsFound: AccessRight[];

  selectedAddRole: Role;
  selectedAddFunction: AccessFunction;
  selectedAddAccessRight: AccessRight;
  selectedAddDeniedRight: AccessRight;

  addRolePressed: boolean;
  addFunctionPressed: boolean;
  addAccessRightPressed: boolean;
  addDeniedRightPressed: boolean;

  id: string;

  constructor(private userService: UserRightsService, private functionService: FunctionsService,
    private rightsService: RightsService, private roleService: RoleService, private router: ActivatedRoute,
    private route: Router, private formBuilder: FormBuilder) {
    router.params.subscribe(params => this.id = params.id);
    console.log(this.id);
   }

  ngOnInit() {
    this.userForm = this.formBuilder.group({
      roleNameSearch: [''],
      functionNameSearch: [''],
      accessRightNameSearch: [''],
      deniedRightNameSearch: ['']
  });
  this.userService.userRightsGetUserRights( this.id ).subscribe(user => {
    this.errorMessage = user.message;
    this.user = user.userRights; });
  }
  onSelectRole(role: Role) {
    this.selectedRole = role;
  }
  onSelectFunction(fun: AccessFunction) {
    this.selectedFunction = fun;
  }
  onSelectAccessRight(right: AccessRight) {
    this.selectedAccessRight = right;
  }
  onSelectDeniedRight(right: AccessRight) {
    this.selectedDeniedRight = right;
  }
  onAddRole() {
    this.addRolePressed = !this.addRolePressed;
  }
  onAddFunction() {
    this.addFunctionPressed = !this.addFunctionPressed;
  }
  onAddAccessRight() {
    this.addAccessRightPressed = !this.addAccessRightPressed;
  }
  onAddDeniedRight() {
    this.addDeniedRightPressed = !this.addDeniedRightPressed;
  }
  onRoleSearch() {
    this.roleService.roleGetRoles(5, 1, this.userForm.get('roleNameSearch').value).subscribe(fun => {
      this.errorMessage = fun.message;
      this.rolesFound = fun.roles; });
  }
  onFunctionSearch() {
    this.functionService.functionsGetFunctions(5, 1, this.userForm.get('functionNameSearch').value).subscribe(fun => {
      this.errorMessage = fun.message;
      this.functionsFound = fun.accessFunctions; });
  }
  onAccessRightSearch() {
    this.rightsService.rightsGetRights(5, 1, this.userForm.get('accessRightNameSearch').value).subscribe(fun => {
      this.errorMessage = fun.message;
      this.accessRightsFound = fun.accessRights; });
  }
  onDeniedRightSearch() {
    this.rightsService.rightsGetRights(5, 1, this.userForm.get('deniedRightNameSearch').value).subscribe(fun => {
      this.errorMessage = fun.message;
      this.deniedRightsFound = fun.accessRights; });
  }
  onAddRoleSelected(role: Role) {
    this.selectedAddRole = role;
    this.userForm.controls.roleNameSearch.setValue(role.name);
  }
  onAddFunctionSelected(fun: AccessFunction) {
    this.selectedAddFunction = fun;
    this.userForm.controls.functionNameSearch.setValue(fun.name);
  }
  onAddAccessRightSelected(right: AccessRight) {
    this.selectedAddAccessRight = right;
    this.userForm.controls.accessRightNameSearch.setValue(right.name);
  }
  onAddDeniedRightSelected(right: AccessRight) {
    this.selectedAddDeniedRight = right;
    this.userForm.controls.deniedRightNameSearch.setValue(right.name);
  }
  onDeleteRole() {
    const roles = this.user.roles
    .filter(fun => fun.id !== this.selectedRole.id);
    const functions = this.user.accessFunctions;
    const accessRights = this.user.accessRights;
    const deniedRights = this.user.deniedRights;
    this.saveUser(roles, functions, accessRights, deniedRights);
    this.selectedRole = null;
  }
  onDeleteFunction() {
    const roles = this.user.roles;
    const functions = this.user.accessFunctions
    .filter(fun => fun.id !== this.selectedFunction.id);
    const accessRights = this.user.accessRights;
    const deniedRights = this.user.deniedRights;
    this.saveUser(roles, functions, accessRights, deniedRights);
    this.selectedFunction = null;
  }
  onDeleteAccessRight() {
    const roles = this.user.roles;
    const functions = this.user.accessFunctions;
    const accessRights = this.user.accessRights
    .filter(right => right.id !== this.selectedAccessRight.id);
    const deniedRights = this.user.deniedRights;
    this.saveUser(roles, functions, accessRights, deniedRights);
    this.selectedAccessRight = null;
  }
  onDeleteDeniedRight() {
    const roles = this.user.roles;
    const functions = this.user.accessFunctions;
    const accessRights = this.user.accessRights;
    const deniedRights = this.user.deniedRights
    .filter(right => right.id !== this.selectedDeniedRight.id);
    this.saveUser(roles, functions, accessRights, deniedRights);
    this.selectedDeniedRight = null;
  }
  onAddSelectedRole() {
    let roles: Role[] = [this.selectedAddRole];
    roles = roles.concat(this.user.roles);
    const functions = this.user.accessFunctions;
    const accessRights = this.user.accessRights;
    const deniedRights = this.user.deniedRights;
    this.saveUser(roles, functions, accessRights, deniedRights);
    this.selectedAddRole = null;
    this.userForm.controls.functionNameSearch.setValue('');
  }
  onAddSelectedFunction() {
    const roles = this.user.roles;
    let functions: AccessFunction[] = [this.selectedAddFunction];
    functions = functions.concat(this.user.accessFunctions);
    const accessRights = this.user.accessRights;
    const deniedRights = this.user.deniedRights;
    this.saveUser(roles, functions, accessRights, deniedRights);
    this.selectedAddFunction = null;
    this.userForm.controls.functionNameSearch.setValue('');
  }
  onAddSelectedAccessRight() {
    const roles = this.user.roles;
    const functions = this.user.accessFunctions;
    let accessRights: AccessRight[] = [this.selectedAddAccessRight];
    accessRights = accessRights.concat(this.user.accessRights);
    const deniedRights = this.user.deniedRights;
    this.saveUser(roles, functions, accessRights, deniedRights);
    this.selectedAddAccessRight = null;
    this.userForm.controls.accessRightNameSearch.setValue('');
  }
  onAddSelectedDeniedRight() {
    const roles = this.user.roles;
    const functions = this.user.accessFunctions;
    const accessRights = this.user.accessRights;
    let deniedRights: AccessRight[] = [this.selectedAddDeniedRight];
    deniedRights = deniedRights.concat(this.user.deniedRights);
    this.saveUser(roles, functions, accessRights, deniedRights);
    this.selectedAddDeniedRight = null;
    this.userForm.controls.deniedRightNameSearch.setValue('');
  }
  saveUser(roles: Role[], functions: AccessFunction[], accessRights: AccessRight[], deniedRights: AccessRight[]) {
    const request: EditUserRightsRequest = {
      userId: this.user.userId,
      roleIds: roles.map(function(a) { return a.id; }),
      functionIds: functions.map(function(a) { return a.id; }),
      accessedRightsIds: accessRights.map(function(a) { return a.id; }),
      deniedRightsIds: deniedRights.map(function(a) { return a.id; })
    };
    this.userService.userRightsEditUserRights(request).subscribe(resp => {
      this.errorMessage = resp.message;
      if (resp.status === 0) {
        this.user.roles = roles;
        this.user.accessFunctions = functions;
        this.user.accessRights = accessRights;
        this.user.deniedRights = deniedRights;
      } });
  }
}
