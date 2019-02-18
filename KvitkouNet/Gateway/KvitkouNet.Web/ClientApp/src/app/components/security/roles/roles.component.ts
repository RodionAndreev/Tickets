import { FunctionsService } from 'src/app/services/security/functions.service';
import { AccessFunction } from './../../../models/security/accessFunction';
import { EditRoleRequest } from './../../../models/security/editRoleRequest';
import { Role } from './../../../models/security/role';
import { RoleService } from 'src/app/services/security/role.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FeatureService, RightsService } from 'src/app/services/security/api';
import { AccessRight } from 'src/app/models/security/models';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {
  rolesForm: FormGroup;
  roles: Role[];
  errorMessage: string;
  selectedRole: Role;
  selectedFunction: AccessFunction;
  selectedAccessRight: AccessRight;
  selectedDeniedRight: AccessRight;
  functionsFound: AccessFunction[];
  accessRightsFound: AccessRight[];
  deniedRightsFound: AccessRight[];
  selectedAddFunction: AccessFunction;
  selectedAddAccessRight: AccessRight;
  selectedAddDeniedRight: AccessRight;
  addFunctionPressed: boolean;
  addAccessRightPressed: boolean;
  addDeniedRightPressed: boolean;
  pages: number;
  Arr = Array;
  currentPage: number;
  constructor(private functionService: FunctionsService, private rightsService: RightsService,
    private roleService: RoleService, private formBuilder: FormBuilder) {

   }

   ngOnInit() {
    this.rolesForm = this.formBuilder.group({
      roleName: [''],
      functionNameSearch: [''],
      accessRightNameSearch: [''],
      deniedRightNameSearch: ['']
  });
  }
  onSearchRoles() {
    this.onSearchPage(1);
  }
  onSearchPage(page: number) {
    this.roleService.roleGetRoles(10, page, this.rolesForm.get('roleName').value).subscribe(roles => {
      this.errorMessage = roles.message;
      this.roles = roles.roles;
      this.pages = Math.ceil(roles.totalCount / 10); });
      this.currentPage = page;
    this.selectedRole = null;
    this.addFunctionPressed = false;
    this.addAccessRightPressed = false;
    this.addDeniedRightPressed = false;
    this.selectedAddFunction = null;
    this.selectedAddAccessRight = null;
    this.selectedAddDeniedRight = null;
    this.selectedFunction = null;
    this.selectedAccessRight = null;
    this.selectedDeniedRight = null;
    this.functionsFound = null;
    this.accessRightsFound = null;
    this.deniedRightsFound = null;
  }
  onNextPage() {
    this.onSearchPage(this.currentPage + 1);
  }
  onPreviosePage() {
    this.onSearchPage(this.currentPage - 1);
  }
  onSelectRole(role: Role) {
    this.selectedRole = role;
    this.addFunctionPressed = false;
    this.addAccessRightPressed = false;
    this.addDeniedRightPressed = false;
    this.selectedAddFunction = null;
    this.selectedAddAccessRight = null;
    this.selectedAddDeniedRight = null;
    this.selectedFunction = null;
    this.selectedAccessRight = null;
    this.selectedDeniedRight = null;
    this.functionsFound = null;
    this.accessRightsFound = null;
    this.deniedRightsFound = null;
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
  onAddFunction() {
    this.addFunctionPressed = !this.addFunctionPressed;
  }
  onAddAccessRight() {
    this.addAccessRightPressed = !this.addAccessRightPressed;
  }
  onAddDeniedRight() {
    this.addDeniedRightPressed = !this.addDeniedRightPressed;
  }
  onFunctionSearch() {
    this.functionService.functionsGetFunctions(5, 1, this.rolesForm.get('functionNameSearch').value).subscribe(fun => {
      this.errorMessage = fun.message;
      this.functionsFound = fun.accessFunctions; });
  }
  onAccessRightSearch() {
    this.rightsService.rightsGetRights(5, 1, this.rolesForm.get('accessRightNameSearch').value).subscribe(fun => {
      this.errorMessage = fun.message;
      this.accessRightsFound = fun.accessRights; });
  }
  onDeniedRightSearch() {
    this.rightsService.rightsGetRights(5, 1, this.rolesForm.get('deniedRightNameSearch').value).subscribe(fun => {
      this.errorMessage = fun.message;
      this.deniedRightsFound = fun.accessRights; });
  }
  onAddFunctionSelected(fun: AccessFunction) {
    this.selectedAddFunction = fun;
    this.rolesForm.controls.functionNameSearch.setValue(fun.name);
  }
  onAddAccessRightSelected(right: AccessRight) {
    this.selectedAddAccessRight = right;
    this.rolesForm.controls.accessRightNameSearch.setValue(right.name);
  }
  onAddDeniedRightSelected(right: AccessRight) {
    this.selectedAddDeniedRight = right;
    this.rolesForm.controls.deniedRightNameSearch.setValue(right.name);
  }
  onDeleteFunction() {
    const functions = this.selectedRole.accessFunctions
    .filter(fun => fun.id !== this.selectedFunction.id);
    const accessRights = this.selectedRole.accessRights;
    const deniedRights = this.selectedRole.deniedRights;
    this.saveRights(functions, accessRights, deniedRights);
    this.selectedFunction = null;
  }
  onDeleteAccessRight() {
    const functions = this.selectedRole.accessFunctions;
    const accessRights = this.selectedRole.accessRights
    .filter(right => right.id !== this.selectedAccessRight.id);
    const deniedRights = this.selectedRole.deniedRights;
    this.saveRights(functions, accessRights, deniedRights);
    this.selectedAccessRight = null;
  }
  onDeleteDeniedRight() {
    const functions = this.selectedRole.accessFunctions;
    const accessRights = this.selectedRole.accessRights;
    const deniedRights = this.selectedRole.deniedRights
    .filter(right => right.id !== this.selectedDeniedRight.id);
    this.saveRights(functions, accessRights, deniedRights);
    this.selectedDeniedRight = null;
  }
  onAddSelectedFunction() {
    let functions: AccessFunction[] = [this.selectedAddFunction];
    functions = functions.concat(this.selectedRole.accessFunctions);
    const accessRights = this.selectedRole.accessRights;
    const deniedRights = this.selectedRole.deniedRights;
    this.saveRights(functions, accessRights, deniedRights);
    this.selectedAddFunction = null;
    this.rolesForm.controls.functionNameSearch.setValue('');
  }
  onAddSelectedAccessRight() {
    const functions = this.selectedRole.accessFunctions;
    let accessRights: AccessRight[] = [this.selectedAddAccessRight];
    accessRights = accessRights.concat(this.selectedRole.accessRights);
    const deniedRights = this.selectedRole.deniedRights;
    this.saveRights(functions, accessRights, deniedRights);
    this.selectedAddAccessRight = null;
    this.rolesForm.controls.accessRightNameSearch.setValue('');
  }
  onAddSelectedDeniedRight() {
    const functions = this.selectedRole.accessFunctions;
    const accessRights = this.selectedRole.accessRights;
    let deniedRights: AccessRight[] = [this.selectedAddDeniedRight];
    deniedRights = deniedRights.concat(this.selectedRole.deniedRights);
    this.saveRights(functions, accessRights, deniedRights);
    this.selectedAddDeniedRight = null;
    this.rolesForm.controls.deniedRightNameSearch.setValue('');
  }
  saveRights(functions: AccessFunction[], accessRights: AccessRight[], deniedRights: AccessRight[]) {
    const request: EditRoleRequest = {
      roleId: this.selectedRole.id,
      functionIds: functions.map(function(a) { return a.id; }),
      accessRightsIds: accessRights.map(function(a) { return a.id; }),
      deniedRightsIds: deniedRights.map(function(a) { return a.id; })
    };
    this.roleService.roleEditRole(request).subscribe(roles => {
      this.errorMessage = roles.message;
      if (roles.status === 0) {
        this.selectedRole.accessFunctions = functions;
        this.selectedRole.accessRights = accessRights;
        this.selectedRole.deniedRights = deniedRights;
      } });
  }  
  onDeleteRole(role: Role) {
    this.roleService.roleDeleteRole(role.id).subscribe(result => {
      this.errorMessage = result.message;
      if (result.status === 0) {
      this.roles = this.roles.filter(r => r.id !== role.id);
      this.selectedRole = null;
     }});
  }
}
