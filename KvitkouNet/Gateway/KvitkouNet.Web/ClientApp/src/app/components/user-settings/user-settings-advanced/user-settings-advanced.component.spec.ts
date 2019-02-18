import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSettingsAdvancedComponent } from './user-settings-advanced.component';

describe('UserSettingsAdvancedComponent', () => {
  let component: UserSettingsAdvancedComponent;
  let fixture: ComponentFixture<UserSettingsAdvancedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserSettingsAdvancedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserSettingsAdvancedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
