import { TestBed } from '@angular/core/testing';

import { DebitCardService } from './debit-card.service';

describe('DebitCardService', () => {
  let service: DebitCardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DebitCardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
