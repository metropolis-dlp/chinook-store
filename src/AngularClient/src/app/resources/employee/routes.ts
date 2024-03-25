import {Route} from "@angular/router";
import {EmployeeListComponent} from "./list/employee-list.component";

export const EMPLOYEE_ROUTES: Route[] = [
  { path: 'list', component: EmployeeListComponent },
  { path: '',   redirectTo: 'list', pathMatch: 'full' }
];
