import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DealLogsComponent } from './deal-logs.component';

describe('DealLogsComponent', () => {
  let component: DealLogsComponent;
  let fixture: ComponentFixture<DealLogsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DealLogsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
