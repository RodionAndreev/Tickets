import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSettingsEmailComponent } from './user-settings-email.component';

describe('UserSettingsEmailComponent', () => {
  let component: UserSettingsEmailComponent;
  let fixture: ComponentFixture<UserSettingsEmailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserSettingsEmailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserSettingsEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
