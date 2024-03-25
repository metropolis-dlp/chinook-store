import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {PaginationResultModel} from "../../common/model/pagination-result.model";
import {UserModel} from "./user.model";
import {HttpParams} from "@angular/common/http";
import {BaseService} from "../../common/services/base.service";

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService<UserModel> {
  baseUri = 'api/v1/users';

  query(search: string | null, page: number, size: number, sort: string, ascending: boolean)
    : Observable<PaginationResultModel<UserModel>> {

    let params = new HttpParams()
      .set('page', page)
      .set('size', size)
      .set('sort', sort)
      .set('asc', ascending);

      if (search) {
        params = params.set('search', search);
      }

    return this.client.get<PaginationResultModel<UserModel>>(`${this.baseUri}/query`, { params: params });
  }
}
