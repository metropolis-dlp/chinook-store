import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {PaginationResultModel} from "../../common/model/pagination-result.model";
import {UserModel} from "./user.model";
import { HttpParams } from "@angular/common/http";
import {BaseService} from "../../common/services/base.service";
import {PaginationRequestModel} from "../../common/model/pagination-request.model";

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService<UserModel> {
  baseUri = 'api/v1/users';
}
