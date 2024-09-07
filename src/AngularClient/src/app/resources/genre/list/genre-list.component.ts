import {AfterViewInit, Component, ElementRef, Inject, OnInit, QueryList, ViewChild, ViewChildren} from '@angular/core';
import {CurrencyPipe} from "@angular/common";
import {DurationPipe} from "../../../common/pipes/duration.pipe";
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
import {MatButton, MatIconButton} from "@angular/material/button";
import {BasicItemModel} from "../../../common/model/basic-item.model";
import {MatCheckbox} from "@angular/material/checkbox";
import {
  MAT_DIALOG_DATA, MatDialog,
  MatDialogActions, MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from "@angular/material/dialog";
import {MatOption} from "@angular/material/autocomplete";
import {MatRadioButton, MatRadioGroup} from "@angular/material/radio";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {GenreService} from "../genre.service";
import {ConfirmDialogComponent} from "../../../common/components/confirm-dialog/confirm-dialog.component";
import {ConfirmDialogModel} from "../../../common/components/confirm-dialog/confirm-dialog.model";
import {EMPTY, iif, switchMap, takeUntil} from "rxjs";
import {BaseComponent} from "../../../common/components/base.component";
import { A11yModule } from '@angular/cdk/a11y'

@Component({
  selector: 'genre-list',
  standalone: true,
  imports: [
    CurrencyPipe,
    DurationPipe,
    MatCell,
    MatCellDef,
    MatColumnDef,
    MatHeaderCell,
    MatHeaderRow,
    MatHeaderRowDef,
    MatIcon,
    MatIconButton,
    MatRow,
    MatRowDef,
    MatTable,
    MatHeaderCellDef,
    MatCheckbox,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatButton,
    MatDialogClose,
    MatOption,
    MatRadioButton,
    FormsModule,
    MatRadioGroup,
    ReactiveFormsModule,
    MatFormField,
    MatInput,
    MatLabel,
    A11yModule
  ],
  templateUrl: './genre-list.component.html',
  styleUrl: './genre-list.component.scss'
})
export class GenreListComponent extends BaseComponent implements OnInit, AfterViewInit {
  @ViewChild(MatTable) table!: MatTable<BasicItemModel>;
  @ViewChildren(MatRow, {read: ElementRef}) rows!: QueryList<ElementRef<HTMLTableRowElement>>;

  displayedColumns: string[] = [ 'select', 'name', 'options' ];

  elements: BasicItemModel[] = [];
  selected: number | null = null;

  editing: number | null = null;
  editingValue: string | null = null;
  creating: boolean = false;

  constructor(
    public dialogRef: MatDialogRef<GenreListComponent>,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: { genres: BasicItemModel[], selected: number | null },
    private genreService: GenreService
  ) {
    super();
  }

  ngAfterViewInit(): void {
    this.moveToSelected(this.rows);

    this.rows.changes
      .pipe(takeUntil(this.unsubscriptionNotifier))
      .subscribe(rows => this.moveToSelected(rows));
  }

  private moveToSelected(rows: QueryList<ElementRef<HTMLTableRowElement>>) {
    const nativeSelRow =
      rows.find(row => row.nativeElement.id === this.selected?.toString());
    nativeSelRow?.nativeElement.scrollIntoView({block: 'center', behavior: 'smooth'});
  }

  ngOnInit() {
    this.elements = this.data.genres;
    this.selected = this.data.selected;
  }

  select() {
    this.dialogRef.close(this.selected);
  }

  startCreate() {
    this.creating = true;
  }
  cancelCreate() {
    this.creating = false;
    this.editingValue = null;
  }
  create() {
    this.genreService
      .create({  name: this.editingValue } as BasicItemModel)
      .subscribe(id => {
        this.elements.push({
          id: id,
          name: this.editingValue
        } as BasicItemModel);
        this.elements.sort((a,b) => a.name.localeCompare(b.name));

        this.creating = false;
        this.editingValue = null;
        this.selected = id;

        this.table.renderRows();
      });
  }

  startEdit(id: number) {
    this.editing = id;
    this.editingValue = this.elements.find(e => e.id == id)?.name ?? '';
  }
  cancelEdit() {
    this.editing = null;
    this.editingValue = null;
  }
  update(id: number) {
    this.genreService
      .modify(id, { id: id, name: this.editingValue } as BasicItemModel)
      .subscribe(_ => {
        const element = this.elements.find(e => e.id == id);
        if (element) {
          element.name = this.editingValue ?? '';
        }
        this.elements.sort((a,b) => a.name.localeCompare(b.name));

        this.editing = null;
        this.editingValue = null;
        this.selected = id;

        this.table.renderRows();
      });
  }

  delete(id: number) {
    this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: new ConfirmDialogModel({
        title: 'Delete Genre',
        message: 'Delete this item?'
      })
    }).afterClosed().pipe(
      switchMap(result => iif(() => result == true, this.genreService.delete(id), EMPTY))
    ).subscribe(() => {
      const index = this.elements.findIndex(g => g.id == id);
      if (index >= 0) {
        this.elements.splice(index, 1);
        if (this.selected === id) {
          this.selected = null;
        }
      }

      this.table.renderRows();
    });
  }
}
