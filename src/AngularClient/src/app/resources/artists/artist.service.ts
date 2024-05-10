import { Injectable } from '@angular/core';
import {BaseService} from "../../common/services/base.service";
import {ArtistModel} from "./artist.model";
import {PaginationResultModel} from "../../common/model/pagination-result.model";
import {UserModel} from "../user/user.model";
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArtistService extends BaseService<ArtistModel> {
  baseUri = 'api/v1/artists';

  query(search: string | null, page: number, size: number)
    : Observable<PaginationResultModel<ArtistModel>> {

    let params = new HttpParams()
      .set('page', page)
      .set('size', size);

    if (search) {
      params = params.set('search', search);
    }

    return this.client.get<PaginationResultModel<ArtistModel>>(`${this.baseUri}/query`, { params: params });
  }
}
