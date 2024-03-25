import { Routes } from '@angular/router';

export const routes: Routes = [
  {path: 'user', loadChildren: () => import('./resources/user/routes').then(mod => mod.USER_ROUTES)},
  {path: 'employee', loadChildren: () => import('./resources/employee/routes').then(mod => mod.EMPLOYEE_ROUTES)},
];
