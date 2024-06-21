import {Component, forwardRef, OnInit} from '@angular/core';
import {ArtistModel} from "../../artist/artist.model";
import {BasicItemModel} from "../../../common/model/basic-item.model";
import {
  AbstractControl,
  FormControl,
  FormGroup, NG_VALIDATORS,
  NG_VALUE_ACCESSOR,
  ReactiveFormsModule, ValidationErrors,
  Validator,
  Validators
} from "@angular/forms";
import {MatFormField, MatFormFieldModule} from "@angular/material/form-field";
import {ComboBoxComponent} from "../../../common/components/combo-box/combo-box.component";
import {MatOption, MatSelect} from "@angular/material/select";
import {MatDatepicker, MatDatepickerInput, MatDatepickerToggle} from "@angular/material/datepicker";
import {MatInput} from "@angular/material/input";
import {ArtistService} from "../../artist/artist.service";
import {GenreService} from "../../genre/genre.service";
import {forkJoin, takeUntil, tap} from "rxjs";
import {ReactiveBaseComponent} from "../../../common/components/reactive-base.component";
import {AlbumModel} from "../album.model";

@Component({
  selector: 'album-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormField,
    ComboBoxComponent,
    MatSelect,
    MatOption,
    MatDatepickerInput,
    MatInput,
    MatDatepickerToggle,
    MatDatepicker,
    MatFormFieldModule
  ],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => AlbumFormComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      multi: true,
      useExisting: AlbumFormComponent
    }
  ],
  templateUrl: './album-form.component.html'
})
export class AlbumFormComponent extends ReactiveBaseComponent<AlbumModel> implements OnInit, Validator {
  artists: ArtistModel[] = [];
  genres: BasicItemModel[] = [];

  form = new FormGroup({
    id: new FormControl<number | null>(null),
    title: new FormControl<string>('', Validators.required),
    artistId: new FormControl<number>(0, Validators.required),
    genreId: new FormControl<number>(0, Validators.required),
    releaseDate: new FormControl<Date>({} as Date, Validators.required)
  });

  constructor(
    private artistService: ArtistService,
    private genreService: GenreService
  ) {
    super();
  }

  ngOnInit(): void {
    forkJoin([
      this.artistService.get(),
      this.genreService.get()
    ]).subscribe(([artists, genres]) => {
      this.artists = artists;
      this.genres = genres
    });

    this.form.valueChanges.pipe(
      tap(() => this.onChange(this.value)),
      takeUntil(this.unsubscriptionNotifier)
    ).subscribe();
  }

  override get value(): AlbumModel {
    return this.form.value as AlbumModel;
  }
  override set value(value: AlbumModel) {
    this.form.patchValue(value);
  }

  validate(control: AbstractControl): ValidationErrors | null {
    if (this.form.valid) {
      return null;
    }
    return {
      required: true
    };
  }
}
