import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatTableModule} from "@angular/material/table";
import {MatSort, MatSortModule} from "@angular/material/sort";
import {MatPaginator, MatPaginatorModule} from "@angular/material/paginator";
import {debounce, interval, merge, startWith, switchMap, takeUntil, tap} from "rxjs";
import {FormControl, ReactiveFormsModule} from "@angular/forms";
import {UserService} from "../user.service";
import {UserModel} from "../user.model";
import {BaseComponent} from "../../../common/components/base.component";
import {MatIconModule} from "@angular/material/icon";
import {RouterLink} from "@angular/router";
import {MatButtonModule} from "@angular/material/button";
import {MatInputModule} from "@angular/material/input";

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [
    MatTableModule, MatSortModule,
    MatIconModule,
    RouterLink,
    MatPaginatorModule, MatButtonModule, MatInputModule, ReactiveFormsModule
  ],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent extends BaseComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  filter = new FormControl<string | null>(null);

  displayedColumns: string[] = ['id', 'firstName', 'lastName', 'address', 'country', 'email', 'phone', 'options'];
  elements: UserModel[] = [];
  resultsLength = 0;

  constructor(private userService: UserService) {
    super();
  }

  ngAfterViewInit(): void {
    merge(
      this.sort.sortChange.pipe(tap(_ => this.paginator.pageIndex = 0)),
      this.filter.valueChanges.pipe(
        debounce(() => interval(500)),
        tap(() => this.paginator.pageIndex = 0)),
      this.paginator.page
    ).pipe(
      startWith({}),
      switchMap(() =>
        this.userService.query(
          this.filter.value,
          this.paginator.pageIndex,
          this.paginator.pageSize,
          this.sort.active,
          this.sort.direction == "asc")),
      tap(data => {
        this.resultsLength = data.total;
        this.elements = data.items;
      }),
      takeUntil(this.unsubscriptionNotifier))
      .subscribe();
  }

  delete(id: number) {}
}
