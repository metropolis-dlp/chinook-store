import {Route} from "@angular/router";
import {ArtistListComponent} from "./list/artist-list.component";

export const ARTIST_ROUTES: Route[] = [
  { path: 'list', component: ArtistListComponent },
  { path: '',   redirectTo: 'list', pathMatch: 'full' }
];
