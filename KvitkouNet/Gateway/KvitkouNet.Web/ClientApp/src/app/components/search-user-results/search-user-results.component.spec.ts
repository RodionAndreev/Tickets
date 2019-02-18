import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchUserResultsComponent } from './search-user-results.component';

describe('SearchUserResultsComponent', () => {
  let component: SearchUserResultsComponent;
  let fixture: ComponentFixture<SearchUserResultsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchUserResultsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchUserResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
