export class SearchTicket {
  limit: number;
  offset: number;
  name?: string;
  category?: string;
  city?: string;
  dateFrom?: string;
  dateTo?: string;
  minPrice?: number;
  maxPrice?: number;

  public constructor(init?: Partial<SearchTicket>) {
    Object.assign(this, init);
  }
}
