import {Component} from '@angular/core';
import {MatFormField, MatFormFieldModule, MatLabel} from "@angular/material/form-field";
import {MatInput, MatInputModule} from "@angular/material/input";
import {FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {ComboBoxComponent} from "../../../common/components/combo-box/combo-box.component";
import {MatOption, MatSelect} from "@angular/material/select";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatCard, MatCardActions, MatCardContent, MatCardHeader, MatCardModule} from "@angular/material/card";
import {MatButton} from "@angular/material/button";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {AlbumService} from "../album.service";
import {AlbumModel} from "../album.model";
import {AlbumFormComponent} from "../form/album-form.component";
import {BaseComponent} from "../../../common/components/base.component";

@Component({
  selector: 'app-album-create',
  standalone: true,
  imports: [
    MatFormField,
    MatInput,
    MatLabel,
    ReactiveFormsModule,
    ComboBoxComponent,
    MatCardModule,
    MatCard,
    MatCardHeader,
    MatCardContent,
    MatCardActions,
    MatSelect,
    MatFormFieldModule, MatInputModule, MatDatepickerModule,
    MatOption,
    MatButton, RouterLink, AlbumFormComponent,
  ],
  templateUrl: './album-create.component.html'
})
export class AlbumCreateComponent extends BaseComponent{
  form = new FormControl<AlbumModel>({} as AlbumModel, Validators.required);

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private albumService: AlbumService
  ) {
    super();
  }

  submit() {
    this.albumService.create(this.form.value as AlbumModel)
      .subscribe(() => this.router.navigate(['list'], { relativeTo: this.route.parent }));
  }
}
