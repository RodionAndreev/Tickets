import { TestBed } from '@angular/core/testing';

import { AdvancedSettingsService } from './advanced-settings.service';

describe('AdvancedSettingsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AdvancedSettingsService = TestBed.get(AdvancedSettingsService);
    expect(service).toBeTruthy();
  });
});
