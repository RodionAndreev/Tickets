export class SearchUser {
  limit: number;
  offset: number;
  minRating?: number;

  public constructor(init?: Partial<SearchUser>) {
    Object.assign(this, init);
  }
}
