import {Route} from "@angular/router";
import {UserListComponent} from "./list/user-list.component";

export const USER_ROUTES: Route[] = [
  { path: 'list', component: UserListComponent },
  { path: '',   redirectTo: 'list', pathMatch: 'full' }
];
