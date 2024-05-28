import {Route} from "@angular/router";
import {AlbumListComponent} from "./list/album-list.component";
import {AlbumCreateComponent} from "./create/album-create.component";
import {AlbumEditComponent} from "./edit/album-edit.component";

export const ALBUM_ROUTES: Route[] = [
  { path: 'list', component: AlbumListComponent },
  { path: 'create', component: AlbumCreateComponent },
  { path: 'edit/:id', component: AlbumEditComponent },
  { path: '',   redirectTo: 'list', pathMatch: 'full' }
];
