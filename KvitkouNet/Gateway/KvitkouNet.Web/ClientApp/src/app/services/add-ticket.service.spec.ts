import { TestBed } from '@angular/core/testing';

import { AddTicketService } from './add-ticket.service';

describe('AddTicketService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AddTicketService = TestBed.get(AddTicketService);
    expect(service).toBeTruthy();
  });
});
