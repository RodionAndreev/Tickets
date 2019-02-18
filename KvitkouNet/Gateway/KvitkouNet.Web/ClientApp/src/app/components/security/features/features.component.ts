import { EditFeatureRequest } from './../../../models/security/editFeatureRequest';
import { RightsService } from 'src/app/services/security/rights.service';
import { AccessRight } from './../../../models/security/accessRight';
import { Feature } from './../../../models/security/feature';
import { FeatureService } from 'src/app/services/security/feature.service';
import { Component, OnInit } from '@angular/core';
import { FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-features',
  templateUrl: './features.component.html',
  styleUrls: ['./features.component.css']
})
export class FeaturesComponent implements OnInit {
  featuresForm: FormGroup;
  features: Feature[];
  errorMessage: string;
  selectedFeature: Feature;
  selectedRight: AccessRight;
  rightsFound: AccessRight[];
  selectedAddRight: AccessRight;
  addPressed: boolean;
  pages: number;
  Arr = Array;
  currentPage: number;
  constructor(private featureService: FeatureService, private rightsService: RightsService, private formBuilder: FormBuilder) {

   }

  ngOnInit() {
    this.featuresForm = this.formBuilder.group({
      featureName: [''],
      rightNameSearch: ['']
  });
  }
  onSearchFeatures() {
    this.onSearchPage(1);
  }
  onSearchPage(page: number) {
    this.featureService.featureGetFeatures(10, page, this.featuresForm.get('featureName').value).subscribe(features => {
      this.errorMessage = features.message;
      this.features = features.features;
      this.pages = Math.ceil(features.totalCount / 10); });
      this.currentPage = page;
    this.selectedFeature = null;
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
  onSelectFeature(feature: Feature) {
    this.selectedFeature = feature;
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
    this.rightsService.rightsGetRights(5, 1, this.featuresForm.get('rightNameSearch').value).subscribe(features => {
      this.errorMessage = features.message;
      this.rightsFound = features.accessRights; });
  }
  onAddRightSelected(right: AccessRight) {
    this.selectedAddRight = right;
    this.featuresForm.controls.rightNameSearch.setValue(right.name);
  }
  onDeleteRight() {
    const rights = this.selectedFeature.availableAccessRights
    .filter(right => right.id !== this.selectedRight.id);
    this.saveRights(rights);
    this.selectedRight = null;
  }
  onAddSelectedRight() {
    let rights: AccessRight[] = [this.selectedAddRight];
    rights = rights.concat(this.selectedFeature.availableAccessRights);
    this.saveRights(rights);
    this.selectedAddRight = null;
    this.featuresForm.controls.rightNameSearch.setValue('');
  }
  saveRights(rights: AccessRight[]) {
    const request: EditFeatureRequest = {
      featureId: this.selectedFeature.id,
      rightsIds: rights.map(function(a) { return a.id; })
    };
    this.featureService.featureEditFeature(request).subscribe(features => {
      this.errorMessage = features.message;
      if (features.status === 0) {
        this.selectedFeature.availableAccessRights = rights;
      } });
  }
  onDeleteFeature(feature: Feature) {
    this.featureService.featureDeleteFeature(feature.id).subscribe(result => {
      this.errorMessage = result.message;
      if (result.status === 0) {
      this.features = this.features.filter(r => r.id !== feature.id);
      this.selectedFeature = null;
     }});
  }
}
