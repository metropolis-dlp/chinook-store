import {BaseComponent} from "./base.component";
import {ControlValueAccessor} from "@angular/forms";
import {Component} from "@angular/core";

@Component({
  template: '',
  standalone: true
})
export abstract class ReactiveBaseComponent<T> extends BaseComponent implements ControlValueAccessor {
  public onChange = (_: T) => {};
  public onTouched = () => {};

  public abstract get value(): T;
  public abstract set value(value: T);

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any) {
    this.onTouched = fn;
  }

  writeValue(value: T): void {
    this.value = value;
  }
}
