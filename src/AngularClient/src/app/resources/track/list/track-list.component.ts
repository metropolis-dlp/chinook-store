import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {
  MatCell,
  MatCellDef,
  MatColumnDef,
  MatHeaderCell, MatHeaderCellDef,
  MatHeaderRow,
  MatHeaderRowDef,
  MatRow, MatRowDef, MatTable
} from "@angular/material/table";
import {MatIcon} from "@angular/material/icon";
import {MatIconButton} from "@angular/material/button";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {MatSort, MatSortHeader} from "@angular/material/sort";
import {TrackModel} from "../track.model";
import {BaseComponent} from "../../../common/components/base.component";
import {TrackService} from "../track.service";
import {EMPTY, iif, switchMap, takeUntil, tap} from "rxjs";
import {DurationPipe} from "../../../common/pipes/duration.pipe";
import {CurrencyPipe} from "@angular/common";
import {MatDialog} from "@angular/material/dialog";
import {ArtistCreateEditComponent} from "../../artist/create-edit/artist-create-edit.component";
import {TrackCreateComponent} from "../create/track-create.component";
import {ConfirmDialogComponent} from "../../../common/components/confirm-dialog/confirm-dialog.component";
import {ConfirmDialogModel} from "../../../common/components/confirm-dialog/confirm-dialog.model";
import {TrackEditComponent} from "../edit/track-edit.component";

@Component({
  selector: 'track-list',
  standalone: true,
  imports: [
    MatCell,
    MatCellDef,
    MatColumnDef,
    MatHeaderCell,
    MatHeaderRow,
    MatHeaderRowDef,
    MatIcon,
    MatIconButton,
    MatPaginator,
    MatRow,
    MatRowDef,
    MatSort,
    MatSortHeader,
    MatTable,
    MatHeaderCellDef,
    DurationPipe,
    CurrencyPipe
  ],
  templateUrl: './track-list.component.html'
})
export class TrackListComponent extends BaseComponent implements OnInit {
  @Input() albumId!: number;

  displayedColumns: string[] = ['number', 'name', 'composer', 'length', 'price', 'mediaType', 'options'];
  elements: TrackModel[] = [];

  constructor(
    public dialog: MatDialog,
    private trackService: TrackService
  ) {
    super();
  }

  ngOnInit(): void {
    this.trackService.getByAlbum(this.albumId)
        .subscribe(tracks => this.elements = tracks);
  }

  create() {
    this.dialog.open(TrackCreateComponent, {
      width: '500px',
      disableClose: true,
      data: { albumId: this.albumId }
    }).afterClosed().pipe(
      switchMap(result => iif(() => result, this.trackService.getByAlbum(this.albumId), EMPTY)),
      tap(result => this.elements = result)
    ).subscribe();
  }

  edit(id: number) {
    this.dialog.open(TrackEditComponent, {
      width: '500px',
      disableClose: true,
      data: { id: id }
    }).afterClosed().pipe(
      switchMap(result => iif(() => result, this.trackService.getByAlbum(this.albumId), EMPTY)),
      tap(result => this.elements = result)
    ).subscribe();
  }

  delete(id: number) {
    this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: new ConfirmDialogModel({
        title: 'Delete artist',
        message: 'Delete this item?'
      })
    }).afterClosed().pipe(
      switchMap(result => iif(() => result, this.trackService.delete(id), EMPTY)),
      switchMap(() => this.trackService.getByAlbum(this.albumId)),
      tap(result => this.elements = result)
    ).subscribe();
  }
}
