import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewFunctionComponent } from './new-function.component';

describe('NewFunctionComponent', () => {
  let component: NewFunctionComponent;
  let fixture: ComponentFixture<NewFunctionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewFunctionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewFunctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
