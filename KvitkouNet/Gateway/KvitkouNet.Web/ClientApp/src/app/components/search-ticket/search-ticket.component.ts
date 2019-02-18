import { mergeMap } from 'rxjs/operators';
import { SearchTicket } from './../../models/searchTicket';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { SearchService } from './../../services/search.service';
import { Observable, BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-search-ticket',
  templateUrl: './search-ticket.component.html',
  styleUrls: ['./search-ticket.component.css']
})
export class SearchTicketComponent implements OnInit {
  searchTicketForm = new FormGroup({
    name: new FormControl(''),
    category: new FormControl(''),
    city: new FormControl(''),
    dateFrom: new FormControl(''),
    dateTo: new FormControl(''),
    minPrice: new FormControl(''),
    maxPrice: new FormControl('')
  });
  error: boolean;
  authenticated: boolean;

  constructor(private router: Router, private service: SearchService) {
    this.authenticated = this.service.isAuthenticated();
  }

  ngOnInit() {}

  onSubmit() {
    const request: SearchTicket = Object.assign(
      {},
      this.searchTicketForm.value
    );
    this.clearNullFields(request);
    this.router.navigate(['search-ticket-results', request]);
  }

  previousSearch() {
    const userId: Observable<string> = new BehaviorSubject('user');
    this.service.getPreviousTicketSearch()
      .subscribe(
        result => {
          this.clearNullFields(result);
          this.router.navigate(['search-ticket-results', result]);
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
