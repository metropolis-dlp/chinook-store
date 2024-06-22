import {AfterViewInit, Component, ViewChild } from '@angular/core';
import {ArtistModel} from "../artist.model";
import {MatPaginator, MatPaginatorModule, PageEvent} from '@angular/material/paginator';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {BaseComponent} from "../../../common/components/base.component";
import {ArtistService} from "../artist.service";
import {debounce, EMPTY, iif, interval, merge, startWith, switchMap, takeUntil, tap} from 'rxjs';
import {MatInputModule} from "@angular/material/input";
import {MatTableModule} from "@angular/material/table";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatDialog} from "@angular/material/dialog";
import {ArtistCreateEditComponent} from "../create-edit/artist-create-edit.component";
import {ConfirmDialogComponent} from "../../../common/components/confirm-dialog/confirm-dialog.component";
import {ConfirmDialogModel} from "../../../common/components/confirm-dialog/confirm-dialog.model";
import {MatSort, MatSortModule} from "@angular/material/sort";
import {PaginationRequestModel} from "../../../common/model/pagination-request.model";

@Component({
  selector: 'app-artist-list',
  standalone: true,
  imports: [
    MatInputModule,
    ReactiveFormsModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    MatPaginatorModule,
    MatSortModule
  ],
  templateUrl: './artist-list.component.html'
})
export class ArtistListComponent extends BaseComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  filter = new FormControl<string | null>(null);

  displayedColumns: string[] = ['name', 'albums', 'options'];
  elements: ArtistModel[] = [];
  resultsLength = 0;

  constructor(
    public dialog: MatDialog,
    private artistService: ArtistService
  ) {
    super();
  }

  ngAfterViewInit(): void {
    merge(
      this.sort.sortChange,
      this.filter.valueChanges.pipe(
        debounce(() => interval(500)),
        tap(() => this.paginator.pageIndex = 0)),
      this.paginator.page
    ).pipe(
      startWith({}),
      switchMap(() =>
        this.artistService.query(
          new PaginationRequestModel(this.paginator, this.sort, {
            search: this.filter.value
          }))),
      tap(data => {
        this.resultsLength = data.total;
        this.elements = data.items;
      }),
      takeUntil(this.unsubscriptionNotifier))
      .subscribe();
  }

  create() {
    this.dialog.open(ArtistCreateEditComponent, {
      width: '500px',
      disableClose: true
    }).afterClosed().subscribe(result => {
      if (result == true) {
        this.paginator.page.emit(new PageEvent())
      }
    });
  }

  edit(id: number) {
    this.dialog.open(ArtistCreateEditComponent, {
      width: '500px',
      disableClose: true,
      data: { id: id }
    }).afterClosed().subscribe(result => {
      if (result == true) {
        this.paginator.page.emit(new PageEvent());
      }
    });
  }

  delete(id: number) {
    this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: new ConfirmDialogModel({
        title: 'Delete artist',
        message: 'Delete this item?'
      })
    }).afterClosed().pipe(
      switchMap(result => iif(() => result == true, this.artistService.delete(id), EMPTY)),
      tap(() => this.paginator.page.emit(new PageEvent()))
    ).subscribe();
  }
}
