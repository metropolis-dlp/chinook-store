import {HttpParams} from "@angular/common/http";

export abstract class QueryRequestModel {
  filters: {[key: string]: number | string | boolean | null} | null = null;

  constructor(filters: {[key: string]: number | string | boolean | null}) {
    this.filters = filters;
  }

  getParams(): HttpParams {
    let httpParams = new HttpParams();

    if (!this.filters) {
      return httpParams;
    }

    Object.keys(this.filters).forEach(key => {
      const value = this.filters?.[key];
      if (value != null) {
        httpParams = httpParams.set(key, value);
      }
    });

    return httpParams;
  }
}
