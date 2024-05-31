import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import { HttpParams } from "@angular/common/http";
import {retry} from "rxjs";

export class PaginationRequestModel {
  offset: number = 0;
  size: number = 10;
  sort: string = '';
  ascending: boolean = true;

  filters: {[key: string]: number | string | boolean | null} | null = null;

  constructor(paginator: MatPaginator, sort: MatSort, filters: {[key: string]: number | string | boolean | null}) {
    this.offset = paginator.pageIndex * paginator.pageSize;
    this.size =  paginator.pageSize;
    this.sort = sort.active;
    this.ascending = sort.direction == "asc";

    this.filters = filters;
  }

  getParams(): HttpParams {
    let result = new HttpParams()
      .set('offset', this.offset)
      .set('size', this.size)
      .set('sort', this.sort)
      .set('asc', this.ascending);

    if (!this.filters) {
      return result;
    }

    Object.keys(this.filters).forEach(key => {
      const value = this.filters?.[key];
      if (value != null) {
        result = result.set(key, value);
      }
    })

    return result;
  }

}
