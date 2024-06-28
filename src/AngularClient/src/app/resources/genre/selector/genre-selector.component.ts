import {Component, ElementRef, Input, OnInit, Optional, Self} from '@angular/core';
import {MatInput, MatSuffix} from "@angular/material/input";
import {MatIcon} from "@angular/material/icon";
import {FormControl, NgControl, ReactiveFormsModule} from "@angular/forms";
import {MatFieldBaseComponent} from "../../../common/components/mat-field.base.component";
import {MatFormField, MatFormFieldControl} from "@angular/material/form-field";
import {FocusMonitor} from "@angular/cdk/a11y";
import {BasicItemModel} from "../../../common/model/basic-item.model";
import {MatDialog} from "@angular/material/dialog";
import {GenreListComponent} from "../list/genre-list.component";
import {GenreService} from "../genre.service";

@Component({
  selector: 'genre-selector',
  standalone: true,
  imports: [
    MatInput,
    MatIcon,
    MatSuffix,
    ReactiveFormsModule
  ],
  templateUrl: './genre-selector.component.html',
  styleUrl: './genre-selector.component.scss',
  providers: [
    {
      provide: MatFormFieldControl,
      useExisting: GenreSelectorComponent
    }
  ],
  inputs: [ 'required', 'placeholder' ]
})
export class GenreSelectorComponent extends MatFieldBaseComponent<number | null> {
  @Input() options: BasicItemModel[] = [];

  get selected(): BasicItemModel | null {
    return this.options.find(o => o.id == this.value) ?? null;
  }

  constructor(
    public dialog: MatDialog,
    @Optional() @Self() ngControl: NgControl,
    elementRef: ElementRef<HTMLInputElement>,
    focusMonitor: FocusMonitor,
    @Optional() public parentFormField: MatFormField
  ) {
    super('genre-selector', ngControl, elementRef, focusMonitor)
  }

  override value: number | null = null;

  override get empty(): boolean {
    return !this.value;
  }

  open() {
    this.dialog.open(GenreListComponent, {
      width: '500px',
      disableClose: true,
      data: { genres: this.options, selected: this.value }
    }).afterClosed().subscribe((result: number | boolean | null) => {
      if (result === false) {
        return;
      }
      this.value = result as number | null;
      this.onChange(this.value);
    });
  }
}
