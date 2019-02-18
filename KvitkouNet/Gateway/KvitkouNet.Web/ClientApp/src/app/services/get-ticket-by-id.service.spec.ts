import { TestBed } from '@angular/core/testing';

import { GetTicketByIdService } from './get-ticket-by-id.service';

describe('GetTicketByIdService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GetTicketByIdService = TestBed.get(GetTicketByIdService);
    expect(service).toBeTruthy();
  });
});
