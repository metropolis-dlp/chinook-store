import {Component, Inject, OnInit} from '@angular/core';
import {MatButton} from "@angular/material/button";
import {
  MAT_DIALOG_DATA,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle
} from "@angular/material/dialog";
import {TrackFormComponent} from "../form/track-form.component";
import {FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {TrackModel} from "../track.model";
import {TrackService} from "../track.service";
import {switchMap, takeUntil, tap} from "rxjs";

@Component({
  selector: 'app-track-edit',
  standalone: true,
  imports: [
    MatButton,
    MatDialogActions,
    MatDialogContent,
    MatDialogTitle,
    TrackFormComponent,
    ReactiveFormsModule,
    MatDialogClose
  ],
  templateUrl: './track-edit.component.html'
})
export class TrackEditComponent implements OnInit {
  form = new FormControl<TrackModel>({} as TrackModel, Validators.required);

  constructor(
    public dialog: MatDialogRef<TrackEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { id: number },
    private trackService: TrackService
  ) {
  }

  ngOnInit(): void {
    this.trackService.get(this.data.id)
      .subscribe(track => this.form.setValue(track));
  }

  submit() {
    this.trackService.modify(this.data.id, this.form.value as TrackModel)
      .subscribe(() => this.dialog.close(true));
  }
}
