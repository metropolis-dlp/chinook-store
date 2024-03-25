import {Observable, Subject, tap} from "rxjs";
import {Component, OnDestroy} from "@angular/core";

@Component({
  template: '',
  standalone: true
})
export abstract class BaseComponent implements OnDestroy {
  protected unsubscriptionNotifier = new Subject<void>();

  ngOnDestroy(): void {
    this.unsubscriptionNotifier.next();
    this.unsubscriptionNotifier.complete();
  }
}
