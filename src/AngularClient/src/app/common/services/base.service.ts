import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {PaginationResultModel} from "../model/pagination-result.model";
import {PaginationRequestModel} from "../model/pagination-request.model";

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService<T> {
  protected abstract baseUri: string;

  protected constructor(protected client: HttpClient) { }

  get(): Observable<T[]>;
  get(id: number): Observable<T>;
  get(id?: number): Observable<T[]> | Observable<T> {
    return id
      ? this.client.get<T>(`${this.baseUri}/${id}`)
      : this.client.get<T[]>(`${this.baseUri}`)
  }

  query(query: PaginationRequestModel): Observable<PaginationResultModel<T>> {
    return this.client.get<PaginationResultModel<T>>(
      `${this.baseUri}/query`,
      { params: query.getParams() });
  }

  create(model: T) {
    return this.client.post(this.baseUri, model);
  }

  modify(id: number, model: T) {
    return this.client.put(`${this.baseUri}/${id}`, model);
  }

  delete(id: number) {
    return this.client.delete(`${this.baseUri}/${id}`);
  }
}
