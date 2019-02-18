import { SearchUser } from './../../models/searchUser';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { SearchService } from './../../services/search.service';

@Component({
  selector: 'app-search-user',
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent implements OnInit {
  searchUserForm = new FormGroup({
    minRating: new FormControl('')
  });
  error: boolean;
  authenticated: boolean;

  constructor(private router: Router, private service: SearchService) {
    this.authenticated = this.service.isAuthenticated();
  }

  ngOnInit() {}

  onSubmit() {
    const request: SearchUser = Object.assign({}, this.searchUserForm.value);
    for (const key in request) {
      if (!request[key]) {
        delete request[key];
      }
    }

    this.router.navigate(['search-user-results', request]);
  }

  previousSearch() {
    const userId: Observable<string> = new BehaviorSubject('user');
    this.service.getPreviousUserSearch().subscribe(
      result => {
        this.clearNullFields(result);
        this.router.navigate(['search-user-results', result]);
      },
      err => {
        console.error(err);
        this.error = true;
      }
    );
  }

  private clearNullFields(obj: any) {
    for (const key in obj) {
      if (!obj[key]) {
        delete obj[key];
      }
    }
    delete obj['id'];
    delete obj['searchTime'];
    delete obj['userId'];
  }
}
