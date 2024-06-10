import { Injectable } from '@angular/core';
import {BaseService} from "../../common/services/base.service";
import {TrackModel} from "./track.model";

@Injectable({
  providedIn: 'root'
})
export class TrackService extends BaseService<TrackModel> {
  baseUri = 'api/v1/tracks';

  getByAlbum(id: number) {
    return this.client.get<TrackModel[]>(`${this.baseUri}?albumId=${id}`);
  }
}
