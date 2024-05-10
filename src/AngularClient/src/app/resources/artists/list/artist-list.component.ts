import {AfterViewInit, Component, ViewChild } from '@angular/core';
import {ArtistModel} from "../artist.model";
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {BaseComponent} from "../../../common/components/base.component";
import {ArtistService} from "../artist.service";
import {debounce, interval, merge, startWith, switchMap, takeUntil, tap } from 'rxjs';
import {MatInputModule} from "@angular/material/input";
import {MatTableModule} from "@angular/material/table";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatDialog} from "@angular/material/dialog";
import {ArtistCreateEditComponent} from "../create-edit/artist-create-edit.component";

@Component({
  selector: 'app-artist-list',
  standalone: true,
  imports: [
    MatInputModule,
    ReactiveFormsModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    MatPaginatorModule
  ],
  templateUrl: './artist-list.component.html',
  styleUrl: './artist-list.component.scss'
})
export class ArtistListComponent extends BaseComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;

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
      this.filter.valueChanges.pipe(
        debounce(() => interval(500)),
        tap(() => this.paginator.pageIndex = 0)),
      this.paginator.page
    ).pipe(
      startWith({}),
      switchMap(() =>
        this.artistService.query(
          this.filter.value,
          this.paginator.pageIndex,
          this.paginator.pageSize)),
      tap(data => {
        this.resultsLength = data.total;
        this.elements = data.items;
      }),
      takeUntil(this.unsubscriptionNotifier))
      .subscribe();
  }

  create() {
    this.dialog.open(ArtistCreateEditComponent, {
      width: '450px',
      disableClose: true
    }).afterClosed().subscribe(result => {
      if (result == true) {
        this.paginator.pageIndex = 0;
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
        this.paginator.pageIndex = 0;
      }
    });
  }

  delete(id: number) {}
}
