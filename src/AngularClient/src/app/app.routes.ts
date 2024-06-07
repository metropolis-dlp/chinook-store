import { Routes } from '@angular/router';

export const routes: Routes = [
  {path: 'artist', loadChildren: () => import('./resources/artists/artist.routes').then(mod => mod.ARTIST_ROUTES)},
  {path: 'album', loadChildren: () => import('./resources/albums/album.routes').then(mod => mod.ALBUM_ROUTES)},
  {path: 'employee', loadChildren: () => import('./resources/employee/employee.routes').then(mod => mod.EMPLOYEE_ROUTES)}
];
