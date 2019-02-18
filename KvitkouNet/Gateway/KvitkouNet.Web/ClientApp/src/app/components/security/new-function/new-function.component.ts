import { AddFunctionRequest } from './../../../models/security/addFunctionRequest';
import { AccessFunction } from './../../../models/security/accessFunction';
import { FunctionsService } from 'src/app/services/security/functions.service';
import { Feature } from './../../../models/security/feature';
import { FeatureService } from 'src/app/services/security/feature.service';
import { Component, OnInit } from '@angular/core';
import { FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-new-function',
  templateUrl: './new-function.component.html',
  styleUrls: ['./new-function.component.css']
})
export class NewFunctionComponent implements OnInit {
  functionForm: FormGroup;
  secceded: boolean;
  errorMessage: string;
  featuresFound: Feature[];
  selectedFature: Feature;
  constructor(private featureService: FeatureService, private functionsService: FunctionsService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.functionForm = this.formBuilder.group({
      functionName: [''],
      featureNameSearch: ['']
  });
  }
  onSearchFeatures() {
    this.featureService.featureGetFeatures(5, 1, this.functionForm.get('featureNameSearch').value).subscribe(features => {
      this.errorMessage = features.message;
      this.featuresFound = features.features; });
  }
  onSelectFeature(feature: Feature) {
    this.selectedFature = feature;
    this.functionForm.controls.featureNameSearch.setValue(feature.name);
  }
  onAddfunction() {
    const request: AddFunctionRequest = {
      featureId: this.selectedFature.id,
      functionName: this.functionForm.get('functionName').value
    };
    this.functionsService.functionsAddFunction(request).subscribe(features => {
      this.secceded = features.status === 0;
      this.errorMessage = features.message; });
  }
  onFunctionNameChanged() {
    this.secceded = false;
  }
}
