import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QueryLogsComponent } from './query-logs.component';

describe('QueryLogsComponent', () => {
  let component: QueryLogsComponent;
  let fixture: ComponentFixture<QueryLogsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QueryLogsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QueryLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
