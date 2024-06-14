import { Injectable } from '@angular/core';
import {BaseService} from "../../common/services/base.service";
import {BasicItemModel} from "../../common/model/basic-item.model";

@Injectable({
  providedIn: 'root'
})
export class MediaTypeService extends BaseService<BasicItemModel> {
  baseUri = 'api/v1/media-types';
}
