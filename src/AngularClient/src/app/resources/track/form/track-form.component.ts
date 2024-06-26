import {Component, forwardRef, OnInit} from '@angular/core';
import {ComboBoxComponent} from "../../../common/components/combo-box/combo-box.component";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatFormField, MatLabel, MatSuffix} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {MatOption} from "@angular/material/autocomplete";
import {MatSelect} from "@angular/material/select";
import {
  AbstractControl,
  FormControl,
  FormGroup, NG_VALIDATORS, NG_VALUE_ACCESSOR,
  ReactiveFormsModule,
  ValidationErrors,
  Validator, Validators
} from "@angular/forms";
import {ReactiveBaseComponent} from "../../../common/components/reactive-base.component";
import {TrackModel} from "../track.model";
import {takeUntil, tap} from "rxjs";
import {MediaTypeService} from "../../media-type/media-type.service";
import {BasicItemModel} from "../../../common/model/basic-item.model";
import {Duration} from "luxon";

@Component({
  selector: 'track-form',
  standalone: true,
  imports: [
      ComboBoxComponent,
      MatDatepicker,
      MatDatepickerInput,
      MatDatepickerToggle,
      MatFormField,
      MatInput,
      MatLabel,
      MatOption,
      MatSelect,
      MatSuffix,
      ReactiveFormsModule
  ],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TrackFormComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      multi: true,
      useExisting: TrackFormComponent
    }
  ],
  templateUrl: './track-form.component.html'
})
export class TrackFormComponent extends ReactiveBaseComponent<TrackModel> implements OnInit, Validator {
  mediaTypes: BasicItemModel[] = [];

  form = new FormGroup({
    id: new FormControl<number>(0),
    number: new FormControl<number>(0, Validators.required),
    name: new FormControl<string>('', Validators.required),
    composer: new FormControl<string>('', Validators.required),
    length: new FormControl<string>('', Validators.required),
    unitPrice: new FormControl<number>(0, Validators.required),
    mediaTypeId: new FormControl<number>(0, Validators.required)
  });

  constructor(
    private mediaTypeService: MediaTypeService
  ) {
    super();
  }

  ngOnInit(): void {
    this.mediaTypeService.get().subscribe(mediaTypes => this.mediaTypes = mediaTypes);

    this.form.valueChanges.pipe(
      tap(() => this.onChange(this.value)),
      takeUntil(this.unsubscriptionNotifier)
    ).subscribe();
  }

  get value(): TrackModel {
    const result = this.form.value as TrackModel;
    result.milliseconds = Duration.fromISOTime(this.form.value.length ?? '00:00:00').toMillis();
    return result;
  }
  override set value(value: TrackModel) {
    this.form.patchValue(value);
    this.form.patchValue({
      length: Duration.fromMillis(value.milliseconds).toFormat("hh':'mm':'ss")
    })
  }

  validate(_: AbstractControl): ValidationErrors | null {
    if (this.form.valid) {
      return null;
    }
    return {
      required: true
    };
  }
}
