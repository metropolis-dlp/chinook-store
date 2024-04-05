import {Route} from "@angular/router";
import {EmployeeListComponent} from "./list/employee-list.component";
import {EmployeeCreateComponent} from "./create/employee-create.component";

export const EMPLOYEE_ROUTES: Route[] = [
  { path: 'list', component: EmployeeListComponent },
  { path: 'create', component: EmployeeCreateComponent },
  { path: '',   redirectTo: 'list', pathMatch: 'full' }
];
