import {Route} from "@angular/router";
import {AlbumListComponent} from "./album-list/album-list.component";

export const ALBUM_ROUTES: Route[] = [
  { path: 'list', component: AlbumListComponent },
  { path: '',   redirectTo: 'list', pathMatch: 'full' }
];
