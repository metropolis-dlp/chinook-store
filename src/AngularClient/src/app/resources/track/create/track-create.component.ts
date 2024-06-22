import {Component, Inject} from '@angular/core';
import {MatButton} from "@angular/material/button";
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from "@angular/material/dialog";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {AlbumModel} from "../../album/album.model";
import {TrackModel} from "../track.model";
import {TrackFormComponent} from "../form/track-form.component";
import {TrackService} from "../track.service";
import {Duration} from "luxon";

@Component({
  selector: 'app-track-create',
  standalone: true,
  imports: [
    MatButton,
    MatDialogActions,
    MatDialogContent,
    MatDialogTitle,
    MatFormField,
    MatInput,
    MatLabel,
    ReactiveFormsModule,
    TrackFormComponent,
    MatDialogClose
  ],
  templateUrl: './track-create.component.html'
})
export class TrackCreateComponent {
  form = new FormControl<TrackModel>({} as TrackModel, Validators.required);

  constructor(
    public dialog: MatDialogRef<TrackCreateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { albumId: number },
    private trackService: TrackService
  ) {
  }

  submit() {
    const track = this.form.value as TrackModel;
    track.albumId = this.data.albumId;
    this.trackService.create(track)
      .subscribe(() => this.dialog.close(true));
  }
}
