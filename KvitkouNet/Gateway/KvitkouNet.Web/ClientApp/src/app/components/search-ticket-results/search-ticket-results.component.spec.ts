import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchTicketResultsComponent } from './search-ticket-results.component';

describe('SearchTicketResultsComponent', () => {
  let component: SearchTicketResultsComponent;
  let fixture: ComponentFixture<SearchTicketResultsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchTicketResultsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchTicketResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
