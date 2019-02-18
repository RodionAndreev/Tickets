import { Feature } from './../../../models/security/feature';
import { FeatureService } from './../../../services/security/feature.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-new-feature',
  templateUrl: './new-feature.component.html',
  styleUrls: ['./new-feature.component.css']
})
export class NewFeatureComponent implements OnInit {
  featureForm: FormGroup;
  secceded: boolean;
  errorMessage: string;
  constructor(private service: FeatureService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.featureForm = this.formBuilder.group({
      featureName: ['']
  });
  }
  onAddfeature() {
    console.log(this.featureForm.get('featureName').value);
    this.service.featureAddFeature('"' + this.featureForm.get('featureName').value + '"').subscribe(result => {
      this.secceded = result.status === 0;
      this.errorMessage = result.message; });
  }
  onFeatureNameChanged() {
    this.secceded = false;
  }
}
