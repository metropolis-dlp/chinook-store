import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {map} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService<T> {
  protected abstract baseUri: string;

  protected constructor(protected client: HttpClient) { }

  getById(id: number) {
    return this.client.get<T>(`${this.baseUri}/${id}`);
  }

  getAll() {
    return this.client.get<T[]>(`${this.baseUri}`);
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
