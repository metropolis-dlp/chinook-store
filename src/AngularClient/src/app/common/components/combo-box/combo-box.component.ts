import {Component, ElementRef, EventEmitter, Input, OnInit, Optional, Output, Self, ViewChild} from '@angular/core';
import {FormControl, NgControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {BasicItemModel} from "../../model/basic-item.model";
import {map, Observable, of, startWith, takeUntil} from "rxjs";
import {MatFormField, MatFormFieldControl} from "@angular/material/form-field";
import {
  MatAutocompleteModule,
  MatAutocompleteSelectedEvent,
  MatAutocompleteTrigger
} from "@angular/material/autocomplete";
import {MatInput, MatInputModule} from "@angular/material/input";
import {MatFieldBaseComponent} from "../mat-field.base.component";
import {FocusMonitor} from "@angular/cdk/a11y";
import {MatIconModule} from "@angular/material/icon";
import {AsyncPipe} from "@angular/common";

@Component({
  selector: 'combo-box',
  standalone: true,
  imports: [
    MatInputModule,
    ReactiveFormsModule,
    MatIconModule,
    MatAutocompleteModule,
    AsyncPipe
  ],
  templateUrl: './combo-box.component.html',
  styleUrl: './combo-box.component.scss',
  providers: [
    {
      provide: MatFormFieldControl,
      useExisting: ComboBoxComponent
    }
  ],
  inputs: [ 'required', 'placeholder' ]
})
export class ComboBoxComponent extends MatFieldBaseComponent<number | null> implements OnInit {
  @ViewChild(MatAutocompleteTrigger, {read: MatAutocompleteTrigger}) inputAutocomplete!: MatAutocompleteTrigger;
  @ViewChild(MatInput) input!: MatInput;

  control = new FormControl<number | string | null>(null);

  @Output() selectionChange = new EventEmitter<number | null>();

  filteredOptions: Observable<BasicItemModel[]> = of([]);
  private _options: BasicItemModel[] = [];
  @Input() set options(value: BasicItemModel[]) {
    this._options = value;

    if (value && value.length > 0) {
      this.filteredOptions = this.control.valueChanges.pipe(
        startWith(null),
        map(idOrName => this.getFilteredByIdOrName(idOrName)),
        takeUntil(this.unsubscriptionNotifier)
      );
    }

    this.control.setValue(this.value);
  }

  constructor(
    @Optional() @Self() ngControl: NgControl,
    elementRef: ElementRef<HTMLInputElement>,
    focusMonitor: FocusMonitor,
    @Optional() public parentFormField: MatFormField
  ) {
    super('combobox-field', ngControl, elementRef, focusMonitor)
  }

  ngOnInit() {
    this.parentFormField?._elementRef?.nativeElement?.addEventListener('click', this.openPanel.bind(this));
  }

  openPanel(event: Event) {
    event.stopPropagation();
    this.inputAutocomplete.openPanel();
    this.input.focus();
  }

  optionSelected(event: MatAutocompleteSelectedEvent) {
    this.onChange(event.option.value);
    this.selectionChange.emit(event.option.value);

    this.ngControl.control?.updateValueAndValidity({ emitEvent: false });
    this.ngControl.control?.markAsTouched();
  }

  private getFilteredByIdOrName(idOrName: string | number | null) {
    if (!idOrName || idOrName == '' || (typeof idOrName == 'number')) {
      return this._options;
    }

    return this._options.filter(o => o.name.toLowerCase().includes(idOrName.toLowerCase()));
  }

  getNameByIdOrName(idOrName: string | number | null) {
    if (!idOrName || idOrName == '') {
      return '';
    }

    if (typeof idOrName == 'number') {
      return this._options.find(o => o.id == idOrName)?.name ?? '';
    }

    return idOrName;
  }

  override get value(): number | null {
    if (!this.control.value || (typeof this.control.value == 'string')) {
      return null;
    }
    return this.control.value as number;
  }
  override set value(value: number | null) {
    this.control.setValue(value, { emitEvent: false });
  }

  override get empty(): boolean {
    return !this.control.value;
  }
}
