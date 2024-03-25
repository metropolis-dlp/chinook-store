import {Injectable} from "@angular/core";
import {BaseService} from "../../common/services/base.service";
import {EmployeeModel} from "./employee.model";

@Injectable({
  providedIn: 'root'
})
export class EmployeeService extends BaseService<EmployeeModel> {
  baseUri = 'api/v1/employees';
}
