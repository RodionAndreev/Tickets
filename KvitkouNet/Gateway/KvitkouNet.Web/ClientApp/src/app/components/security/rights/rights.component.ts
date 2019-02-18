import { RightsService } from './../../../services/security/rights.service';
import { AccessRight } from './../../../models/security/accessRight';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-rights',
  templateUrl: './rights.component.html',
  styleUrls: ['./rights.component.css']
})
export class RightsComponent implements OnInit {
  rightsForm: FormGroup;
  rights: AccessRight[];
  errorMessage: string;
  pages: number;
  currentPage: number;
  Arr = Array;
  constructor(private service: RightsService, private formBuilder: FormBuilder) {

   }

  ngOnInit() {
    this.rightsForm = this.formBuilder.group({
      rightName: ['']
  });
  }
  onSearchRights() {
    this.onSearchRightsPage(1);
  }
  onSearchRightsPage(page: number) {

    this.service.rightsGetRights(12, page, this.rightsForm.get('rightName').value).subscribe(rights => {
      this.errorMessage = rights.message;
      this.rights = rights.accessRights;
      this.pages = Math.ceil(rights.totalCount / 12); });
      this.currentPage = page;
  }
  onNextPage() {
    this.onSearchRightsPage(this.currentPage + 1);
  }
  onPreviosePage() {
    this.onSearchRightsPage(this.currentPage - 1);
  }
  onDeleteRight(right: AccessRight) {
    this.service.rightsDeleteRight(right.id).subscribe(result => {
      this.errorMessage = result.message;
      if (result.status === 0) {
      this.rights = this.rights.filter(r => r.id !== right.id);
     }});
  }
}
