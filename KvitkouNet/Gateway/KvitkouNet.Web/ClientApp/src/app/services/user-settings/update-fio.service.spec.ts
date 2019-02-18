import { TestBed } from '@angular/core/testing';

import { UpdateFioService } from './update-fio.service';

describe('UpdateFioService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UpdateFioService = TestBed.get(UpdateFioService);
    expect(service).toBeTruthy();
  });
});
