import { SearchService } from './../../services/search.service';
import { SearchResult } from './../../models/searchResult';
import { SearchTicket } from './../../models/searchTicket';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap, map } from 'rxjs/operators';
import { SearchTicketInfo } from '../../models/searchTicketInfo';

@Component({
  selector: 'app-search-ticket-results',
  templateUrl: './search-ticket-results.component.html',
  styleUrls: ['./search-ticket-results.component.css']
})
export class SearchTicketResultsComponent implements OnInit {
  defaultLimit = 9;
  tickets: SearchResult<SearchTicketInfo> = new SearchResult<SearchTicketInfo>();
  request: SearchTicket = new SearchTicket({
    offset: 0,
    limit: this.defaultLimit
  });
  error: boolean;
  categoryType = {
    '0': 'Unknown',
    '1': 'Movie',
    '2': 'Concerts',
    '4': 'Theater',
    '8': 'Ballet',
    '16': 'Sport',
    '32': 'Parties',
    '64': 'Trainings',
    '128': 'Exhibitions',
    '256': 'Circus'
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: SearchService
  ) {}

  ngOnInit() {
    const tickets$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        const request = this.getTicketRequest(params);
        const result = this.service.getTickets(request);
        return result.pipe(map(r => ({ request: request, result: r })));
      })
    );
    tickets$.subscribe(
      result => {
        this.request = result.request;
        this.tickets = result.result;
      },
      err => {
        console.error(err);
        this.error = true;
      }
    );
  }

  private getTicketRequest(params: ParamMap) {
    const request = new SearchTicket({
      limit: params.has('limit')
        ? parseInt(params.get('limit'), 10)
        : this.defaultLimit,
      offset: params.has('offset') ? parseInt(params.get('offset'), 10) : 0
    });

    setIfNotNull('name');
    setIfNotNull('category');
    setIfNotNull('city');
    setIfNotNull('dateFrom');
    setIfNotNull('dateTo');
    setIfNotNull('minPrice');
    setIfNotNull('maxPrice');

    return request;

    function setIfNotNull(name: string) {
      if (!!params.get(name)) {
        request[name] = params.get(name);
      }
    }
  }

  pageNumbers(count = 0): number[] {
    return Array(Math.ceil(count / this.request.limit))
      .fill(0)
      .map((x, i) => i + 1);
  }

  isActive(pageNumber = 0): boolean {
    const request = Object.assign({}, this.request);
    return Math.ceil(request.offset / this.request.limit) === pageNumber - 1;
  }

  goTo(pageNumber) {
    const request = Object.assign({}, this.request);
    request.offset = (pageNumber - 1) * request.limit;
    this.router.navigate(['search-ticket-results', request]);
  }
}
