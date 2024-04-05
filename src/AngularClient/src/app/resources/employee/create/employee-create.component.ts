import { Component } from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatSelectModule} from "@angular/material/select";
import {BasicItemModel} from "../../../common/model/basic-item.model";
import {MatNativeDateModule} from "@angular/material/core";
import {UserCreateComponent} from "../../user/create/user-create.component";
import {ComboBoxComponent} from "../../../common/components/combo-box/combo-box.component";

@Component({
  selector: 'employee-create',
  standalone: true,
  imports: [FormsModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, MatNativeDateModule, MatSelectModule, ReactiveFormsModule, UserCreateComponent, ComboBoxComponent],
  templateUrl: './employee-create.component.html',
  styleUrl: './employee-create.component.scss'
})
export class EmployeeCreateComponent {
  form = new FormGroup({
    title: new FormControl<string>('', Validators.required),
    hireDate: new FormControl<Date>({} as Date, Validators.required),
    reportsTo: new FormControl<number | null>(null)
  })

  employees: BasicItemModel[] = [
    { id: 1, name: 'John Smith' },
    { id: 2, name: 'Mary Sue' },
    { id: 3, name: 'Gary Stu' }
  ];
}
