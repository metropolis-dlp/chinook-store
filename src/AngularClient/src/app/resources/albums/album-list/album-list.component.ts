import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator} from "@angular/material/paginator";
import {MatSort, MatSortHeader} from "@angular/material/sort";
import {FormControl, ReactiveFormsModule} from "@angular/forms";
import {AlbumModel} from "../album.model";
import {BaseComponent} from "../../../common/components/base.component";
import {debounce, interval, merge, startWith, switchMap, takeUntil, tap} from "rxjs";
import {PaginationRequestModel} from "../../../common/model/pagination-request.model";
import {AlbumService} from "../album.service";
import {
  MatCell,
  MatCellDef,
  MatColumnDef,
  MatHeaderCell, MatHeaderCellDef,
  MatHeaderRow,
  MatHeaderRowDef,
  MatRow, MatRowDef, MatTable
} from "@angular/material/table";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatIcon} from "@angular/material/icon";
import {MatIconButton} from "@angular/material/button";
import {MatInput} from "@angular/material/input";
import {DatePipe} from "@angular/common";

@Component({
  selector: 'app-album-list',
  standalone: true,
  imports: [
    MatCell,
    MatCellDef,
    MatColumnDef,
    MatFormField,
    MatHeaderCell,
    MatHeaderRow,
    MatHeaderRowDef,
    MatIcon,
    MatIconButton,
    MatInput,
    MatLabel,
    MatPaginator,
    MatRow,
    MatRowDef,
    MatSort,
    MatSortHeader,
    MatTable,
    ReactiveFormsModule,
    MatHeaderCellDef,
    DatePipe
  ],
  templateUrl: './album-list.component.html',
  styleUrl: './album-list.component.scss'
})
export class AlbumListComponent extends BaseComponent implements AfterViewInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  filter = new FormControl<string | null>(null);

  displayedColumns: string[] = ['title', 'artist', 'genre', 'release', 'options'];
  elements: AlbumModel[] = [];
  resultsLength = 0;

  constructor(
    private albumService: AlbumService
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
        this.albumService.query(
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
}
