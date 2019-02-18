import { SearchUserInfo } from './../../models/searchUserInfo';
import { SearchUser } from './../../models/searchUser';
import { SearchService } from './../../services/search.service';
import { SearchResult } from './../../models/searchResult';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap, map, flatMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-search-user-results',
  templateUrl: './search-user-results.component.html',
  styleUrls: ['./search-user-results.component.css']
})
export class SearchUserResultsComponent implements OnInit {
  defaultLimit = 9;
  users: SearchResult<SearchUserInfo> = new SearchResult<SearchUserInfo>();
  request: SearchUser = new SearchUser({ offset: 0, limit: this.defaultLimit });
  error: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: SearchService
  ) {}

  ngOnInit() {
    const users$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => {
        const request = this.getUserRequest(params);
        const result = this.service.getUsers(request);
        return result.pipe(map(r => ({ request: request, result: r })));
      })
    );
    users$.subscribe(
      result => {
        this.request = result.request;
        this.users = result.result;
      },
      err => {
        console.error(err);
        this.error = true;
      }
    );
  }

  private getUserRequest(params: ParamMap) {
    const request = new SearchUser({
      limit: params.has('limit')
        ? parseInt(params.get('limit'), 10)
        : this.defaultLimit,
      offset: params.has('offset') ? parseInt(params.get('offset'), 10) : 0
    });

    setIfNotNull('minRating');

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
    this.router.navigate(['search-user-results', request]);
  }
}
