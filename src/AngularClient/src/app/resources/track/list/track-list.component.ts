import {Component, OnInit} from '@angular/core';
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
import {MatPaginator} from "@angular/material/paginator";
import {MatSort, MatSortHeader} from "@angular/material/sort";
import {TrackModel} from "../track.model";
import {BaseComponent} from "../../../common/components/base.component";
import {TrackService} from "../track.service";
import {takeUntil, tap} from "rxjs";

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
    MatHeaderCellDef
  ],
  templateUrl: './track-list.component.html',
  styleUrl: './track-list.component.scss'
})
export class TrackListComponent extends BaseComponent implements OnInit {
  displayedColumns: string[] = ['number', 'name', 'composer', 'length', 'price', 'mediaType', 'options'];
  elements: TrackModel[] = [];

  constructor(
    private trackService: TrackService
  ) {
    super();
  }

  ngOnInit(): void {
    this.trackService.getByAlbum(1).pipe(
      tap(tracks => this.elements = tracks),
      takeUntil(this.unsubscriptionNotifier)
    ).subscribe();
  }
}
