import { Component } from '@angular/core';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AddressBoxComponent} from "../../../common/components/address-box/address-box.component";

@Component({
  selector: 'user-create',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    AddressBoxComponent
  ],
  templateUrl: './user-create.component.html'
})
export class UserCreateComponent {
  form = new FormGroup({
    firstName: new FormControl<string>('', Validators.required),
    lastName: new FormControl<string>('', Validators.required),

    phone: new FormControl<string | null>(null),
    email: new FormControl<string | null>(null)
  });
}
