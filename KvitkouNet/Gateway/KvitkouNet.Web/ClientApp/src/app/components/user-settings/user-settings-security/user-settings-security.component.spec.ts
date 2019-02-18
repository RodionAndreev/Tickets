import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSettingsSecurityComponent } from './user-settings-security.component';

describe('UserSettingsSecurityComponent', () => {
  let component: UserSettingsSecurityComponent;
  let fixture: ComponentFixture<UserSettingsSecurityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserSettingsSecurityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserSettingsSecurityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
