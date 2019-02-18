import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketLogsComponent } from './ticket-logs.component';

describe('TicketLogsComponent', () => {
  let component: TicketLogsComponent;
  let fixture: ComponentFixture<TicketLogsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketLogsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
