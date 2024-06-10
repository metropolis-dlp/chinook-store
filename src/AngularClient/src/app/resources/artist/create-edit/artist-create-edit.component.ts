import {Component, Inject, OnInit} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {
  MAT_DIALOG_DATA,
  MatDialogActions, MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from "@angular/material/dialog";
import {ArtistService} from "../artist.service";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {BaseComponent} from "../../../common/components/base.component";
import {ArtistModel} from "../artist.model";

@Component({
  selector: 'app-artist-create-edit',
  standalone: true,
  imports: [
    MatDialogTitle,
    ReactiveFormsModule,
    MatDialogContent,
    MatInputModule,
    MatDialogActions,
    MatButtonModule,
    MatDialogClose
  ],
  templateUrl: './artist-create-edit.component.html',
  styleUrl: './artist-create-edit.component.scss'
})
export class ArtistCreateEditComponent extends BaseComponent implements OnInit {
  form = new FormGroup({
    name: new FormControl<string>('', Validators.required)
  });

  constructor(
    public dialog: MatDialogRef<ArtistCreateEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { id: number | null } | null,
    private artistService: ArtistService
  ) {
    super();
  }

  ngOnInit(): void {
    if (!this.data?.id) {
      return;
    }

    this.artistService.get(this.data.id)
      .subscribe(artist => this.form.patchValue(artist));
  }

  submit() {
    if (this.form.invalid) {
      return;
    }

    if (this.data?.id) {
      this.artistService.modify(this.data.id, this.form.value as ArtistModel)
        .subscribe(() => this.dialog.close(true));
    } else {
      this.artistService.create(this.form.value as ArtistModel)
        .subscribe(() => this.dialog.close(true));
    }
  }
}
