import { Routes } from '@angular/router';

export const routes: Routes = [
  {path: 'user', loadChildren: () => import('./resources/user/user.routes').then(mod => mod.USER_ROUTES)},
  {path: 'employee', loadChildren: () => import('./resources/employee/employee.routes').then(mod => mod.EMPLOYEE_ROUTES)},
  {path: 'artist', loadChildren: () => import('./resources/artists/artist.routes').then(mod => mod.ARTIST_ROUTES)},
];
