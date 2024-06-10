import {Routes} from '@angular/router';
import {HomeComponent} from "./static/home.component";
import {ContentNotFoundComponent} from "./static/content-not-found.component";

export const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'artist', loadChildren: () => import('./resources/artist/artist.routes').then(mod => mod.ARTIST_ROUTES)},
  {path: 'album', loadChildren: () => import('./resources/album/album.routes').then(mod => mod.ALBUM_ROUTES)},
  {path: 'employee', loadChildren: () => import('./resources/employee/employee.routes').then(mod => mod.EMPLOYEE_ROUTES)},
  {path: '**', component: ContentNotFoundComponent}
];
