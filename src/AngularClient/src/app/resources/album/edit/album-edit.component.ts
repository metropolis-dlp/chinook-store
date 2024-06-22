import {Component, OnInit} from '@angular/core';
import {AlbumFormComponent} from "../form/album-form.component";
import {MatButton} from "@angular/material/button";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {AlbumModel} from "../album.model";
import {BaseComponent} from "../../../common/components/base.component";
import {AlbumService} from "../album.service";
import {switchMap, takeUntil, tap} from "rxjs";
import {TrackListComponent} from "../../track/list/track-list.component";

@Component({
  selector: 'app-album-edit',
  standalone: true,
  imports: [
    AlbumFormComponent,
    MatButton,
    RouterLink,
    ReactiveFormsModule,
    TrackListComponent
  ],
  templateUrl: './album-edit.component.html'
})
export class AlbumEditComponent extends BaseComponent implements OnInit {
  form = new FormControl<AlbumModel>({} as AlbumModel, Validators.required);
  get id(): number | null {
    return this.form.value?.id ?? null;
  }

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private albumService: AlbumService
  ) {
    super();
  }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => this.albumService.get(Number(params.get('id')))),
      tap(album => this.form.setValue(album)),
      takeUntil(this.unsubscriptionNotifier)
    ).subscribe();
  }

  submit() {
    this.albumService.modify(this.form.value!.id, this.form.value as AlbumModel)
      .subscribe(() => this.router.navigate(['list'], { relativeTo: this.route.parent }));
  }
}
