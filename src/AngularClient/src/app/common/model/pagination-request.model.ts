import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import { HttpParams } from "@angular/common/http";
import {QueryRequestModel} from "./query-request.model";

export class PaginationRequestModel extends QueryRequestModel {
  offset: number = 0;
  size: number = 10;
  sort: string = '';
  ascending: boolean = true;

  constructor(paginator: MatPaginator, sort: MatSort, filters: {[key: string]: number | string | boolean | null}) {

    super(filters);
    this.offset = paginator.pageIndex * paginator.pageSize;
    this.size =  paginator.pageSize;
    this.sort = sort.active;
    this.ascending = sort.direction == "asc";
  }

  override getParams(): HttpParams {
    return super.getParams()
      .set('offset', this.offset)
      .set('size', this.size)
      .set('sort', this.sort)
      .set('asc', this.ascending);
  }
}
