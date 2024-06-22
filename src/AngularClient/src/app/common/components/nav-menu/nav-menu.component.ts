import {Component, OnInit} from '@angular/core';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatListModule} from "@angular/material/list";
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatTooltipModule} from "@angular/material/tooltip";
import {ActivatedRoute, NavigationEnd, Route, Router, RouterLink} from "@angular/router";
import {BaseComponent} from "../base.component";
import {filter, of, switchMap, takeUntil, tap} from "rxjs";
import {routes} from "../../../app.routes";

@Component({
  selector: 'nav-menu',
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatSidenavModule,
    MatToolbarModule,
    MatTooltipModule,
    RouterLink
  ],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.scss'
})
export class NavMenuComponent implements OnInit {
  routeTitle?: string;

  constructor(
    private router: Router)
  {
  }

  ngOnInit() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      switchMap(() => {
        let route: ActivatedRoute = this.router.routerState.root;
        while (route!.firstChild) {
          route = route.firstChild;
        }
        return route.title;
      }),
      tap(title => this.routeTitle = title)
    ).subscribe();
  }
}
