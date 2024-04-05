import {ReactiveBaseComponent} from "./reactive-base.component";
import {ElementRef, HostBinding, Injectable, OnChanges, OnDestroy, Optional, Self, SimpleChanges} from "@angular/core";
import {ControlValueAccessor, NgControl, Validators} from "@angular/forms";
import {MatFormFieldControl} from "@angular/material/form-field";
import {Subject} from "rxjs";
import {FocusMonitor} from "@angular/cdk/a11y";
import {coerceBooleanProperty} from "@angular/cdk/coercion";

@Injectable()
export abstract class MatFieldBaseComponent<T>
  extends ReactiveBaseComponent<T> implements OnDestroy, OnChanges, ControlValueAccessor, MatFormFieldControl<T> {
  static nextId = 0;

  public stateChanges = new Subject<void>();
  public focused = false;

  protected _disabled = false;

  protected constructor(
    public controlType: string,
    @Optional() @Self() public ngControl: NgControl,
    protected elementRef: ElementRef<HTMLInputElement>,
    protected focusMonitor: FocusMonitor
  ) {
    super();

    focusMonitor.monitor(this.elementRef.nativeElement, true).subscribe(origin => {
      this.focused = !!origin;
      this.stateChanges.next();
    });

    if (this.ngControl != null) {
      this.ngControl.valueAccessor = this;
    }
  }

  override ngOnDestroy() {
    super.ngOnDestroy();

    this.stateChanges.complete();
    this.focusMonitor.stopMonitoring(this.elementRef.nativeElement);
  }

  ngOnChanges(_: SimpleChanges) {
    this.stateChanges.next();
  }

  protected focus(): void {
    this.elementRef.nativeElement.focus();
  }

  // -- ControlValueAccessor

  setDisabledState(isDisabled: boolean) {
    this._disabled = isDisabled;
  }

  // -- MatFormFieldControl<T>

  @HostBinding() id = `${this.controlType}-${MatFieldBaseComponent.nextId++}`;
  get shouldLabelFloat(): boolean {
    return this.focused || !this.empty;
  }
  get empty(): boolean {
    return !this.elementRef.nativeElement.value;
  }
  get errorState(): boolean {
    return (this.ngControl.errors !== null) && (this.ngControl?.touched === true);
  }

  get disabled(): boolean {
    if (this.ngControl && this.ngControl.disabled !== null) {
      return this.ngControl.disabled;
    }
    return this._disabled;
  }
  set disabled(value: any) {
    this._disabled = coerceBooleanProperty(value);

    if (this.focused) {
      this.focused = false;
      this.stateChanges.next();
    }
  }

  placeholder = '';
  get required(): boolean {
    return this.ngControl?.control?.hasValidator(Validators.required) ?? false;
  }

  onContainerClick(_: MouseEvent) {
    if (!this.focused) {
      this.focus();
    }
  }

  setDescribedByIds(ids: string[]) {
  }
}
