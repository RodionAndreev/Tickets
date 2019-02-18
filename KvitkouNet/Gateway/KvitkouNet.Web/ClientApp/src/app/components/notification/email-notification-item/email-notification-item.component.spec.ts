import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailNotificationItemComponent } from './email-notification-item.component';

describe('EmailNotificationItemComponent', () => {
  let component: EmailNotificationItemComponent;
  let fixture: ComponentFixture<EmailNotificationItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmailNotificationItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailNotificationItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
