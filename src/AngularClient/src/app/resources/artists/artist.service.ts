import { Injectable } from '@angular/core';
import {BaseService} from "../../common/services/base.service";
import {ArtistModel} from "./artist.model";
import {PaginationResultModel} from "../../common/model/pagination-result.model";
import {UserModel} from "../user/user.model";
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {PaginationRequestModel} from "../../common/model/pagination-request.model";

@Injectable({
  providedIn: 'root'
})
export class ArtistService extends BaseService<ArtistModel> {
  baseUri = 'api/v1/artists';
}

