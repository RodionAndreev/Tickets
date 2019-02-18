import { RightsService } from './../../../services/security/rights.service';
import { AccessRight } from './../../../models/security/accessRight';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-new-right',
  templateUrl: './new-right.component.html',
  styleUrls: ['./new-right.component.css']
})
export class NewRightComponent implements OnInit {
  rightForm: FormGroup;
  secceded: boolean;
  errorMessage: string;
  constructor(private service: RightsService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.rightForm = this.formBuilder.group({
      rightName: ['']
  });
  }
  onAddRight() {
    this.service.rightsAddRights([this.rightForm.get('rightName').value]).subscribe(result => {
      this.secceded = result.status === 0;
      this.errorMessage = result.message; });
  }
  onRightNameChanged() {
    this.secceded = false;
  }
}
