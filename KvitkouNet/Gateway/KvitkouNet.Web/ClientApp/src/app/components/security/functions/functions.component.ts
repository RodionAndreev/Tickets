import { EditFunctionRequest } from './../../../models/security/editFunctionRequest';
import { AccessFunction } from './../../../models/security/accessFunction';
import { FunctionsService } from './../../../services/security/functions.service';
import { EditFeatureRequest } from './../../../models/security/editFeatureRequest';
import { RightsService } from 'src/app/services/security/rights.service';
import { AccessRight } from './../../../models/security/accessRight';
import { Feature } from './../../../models/security/feature';
import { FeatureService } from 'src/app/services/security/feature.service';
import { Component, OnInit } from '@angular/core';
import { FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-functions',
  templateUrl: './functions.component.html',
  styleUrls: ['./functions.component.css']
})
export class FunctionsComponent implements OnInit {
  functionForm: FormGroup;
  functions: AccessFunction[];
  errorMessage: string;
  selectedFunction: AccessFunction;
  selectedRight: AccessRight;
  rightsFound: AccessRight[];
  selectedAddRight: AccessRight;
  addPressed: boolean;
  pages: number;
  Arr = Array;
  currentPage: number;
  constructor(private functionsService: FunctionsService, private rightsService: RightsService,
    private featureService: FeatureService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.functionForm = this.formBuilder.group({
      functionName: [''],
      rightNameSearch: ['']
  });
  }
  onSearchFunctions() {
    this.onSearchPage(1);
  }
  onSearchPage(page: number) {
    this.functionsService.functionsGetFunctions(10, page, this.functionForm.get('functionName').value).subscribe(functions => {
      this.errorMessage = functions.message;
      this.functions = functions.accessFunctions;
      this.pages = Math.ceil(functions.totalCount / 10); });
    this.currentPage = page;
    this.selectedFunction = null;
    this.addPressed = false;
    this.selectedAddRight = null;
    this.selectedRight = null;
    this.rightsFound = null;
  }
  onNextPage() {
    this.onSearchPage(this.currentPage + 1);
  }
  onPreviosePage() {
    this.onSearchPage(this.currentPage - 1);
  }
  onSelectFunction(accessFunction: AccessFunction) {
    this.selectedFunction = accessFunction;
    this.addPressed = false;
    this.selectedAddRight = null;
    this.selectedRight = null;
    this.rightsFound = null;
  }
  onSelectRight(right: AccessRight) {
    this.selectedRight = right;
  }
  onAddRight() {
    this.addPressed = !this.addPressed;
  }
  onAddedSearch() {
    this.featureService.featureGetFeatures(1, 1, this.selectedFunction.featureName).subscribe(features => {
      this.errorMessage = features.message;
      this.rightsFound = features.features[0].availableAccessRights
      .filter(right => right.name.indexOf(this.functionForm.controls.rightNameSearch.value) !== -1); });
  }
  onAddRightSelected(right: AccessRight) {
    this.selectedAddRight = right;
    this.functionForm.controls.rightNameSearch.setValue(right.name);
  }
  onDeleteRight() {
    const rights = this.selectedFunction.accessRights
    .filter(right => right.id !== this.selectedRight.id);
    this.saveRights(rights);
    this.selectedRight = null;
  }
  onAddSelectedRight() {
    let rights: AccessRight[] = [this.selectedAddRight];
    rights = rights.concat(this.selectedFunction.accessRights);
    this.saveRights(rights);
    this.selectedAddRight = null;
    this.functionForm.controls.rightNameSearch.setValue('');
  }
  saveRights(rights: AccessRight[]) {
    const request: EditFunctionRequest = {
      functionId: this.selectedFunction.id,
      rightIds: rights.map(function(a) { return a.id; })
    };
    this.functionsService.functionsEditFunction(request).subscribe(features => {
      this.errorMessage = features.message;
      if (features.status === 0) {
        this.selectedFunction.accessRights = rights;
      } });
  }
  onDeleteFunction(fun: AccessFunction) {
    this.functionsService.functionsDeleteFunction(fun.id).subscribe(result => {
      this.errorMessage = result.message;
      if (result.status === 0) {
      this.functions = this.functions.filter(r => r.id !== fun.id);
      this.selectedFunction = null;
     }});
  }
}
