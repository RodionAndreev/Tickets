import { TestBed, inject } from '@angular/core/testing';

import { GetallticketsService } from './getalltickets.service';

describe('GetallticketsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GetallticketsService]
    });
  });

  it('should be created', inject([GetallticketsService], (service: GetallticketsService) => {
    expect(service).toBeTruthy();
  }));
});
