import {Injectable} from "@angular/core";
import {BaseService} from "../../common/services/base.service";
import {AlbumModel} from "./album.model";

@Injectable({
  providedIn: 'root'
})
export class AlbumService extends BaseService<AlbumModel> {
  baseUri = 'api/v1/albums';
}
