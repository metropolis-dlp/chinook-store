import {Component, OnInit} from '@angular/core';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import {MatSelectModule} from "@angular/material/select";
import {MatInputModule} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatSortModule} from "@angular/material/sort";
import {EmployeeModel} from "../employee.model";
import {DatePipe} from "@angular/common";
import {EmployeeService} from "../employee.service";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'employee-list',
  standalone: true,
  imports: [MatTableModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, MatIconModule, MatPaginatorModule, MatSortModule, DatePipe, RouterLink],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.scss'
})
export class EmployeeListComponent implements OnInit {

  displayedColumns: string[] = [ 'id', 'name', 'title', 'hireDate', 'reportsTo', 'options' ];
  elements = new MatTableDataSource<EmployeeModel>();

  constructor(private employeeService: EmployeeService) {
  }

  ngOnInit(): void {
    this.employeeService.getAll().subscribe(employees => this.elements.data = employees);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.elements.filter = filterValue.trim().toLowerCase();
  }

  delete(id: number) {}
}
