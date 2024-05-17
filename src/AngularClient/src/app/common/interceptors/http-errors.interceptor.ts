import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {catchError, Observable, throwError} from "rxjs";
import {Injectable} from "@angular/core";
import {MatDialog} from "@angular/material/dialog";
import {AlertDialogComponent} from "../components/alert-dialog/alert-dialog.component";
import {AlertDialogModel, AlertDialogType} from "../components/alert-dialog/alert-dialog.model";

@Injectable()
export class HttpErrorsInterceptor implements HttpInterceptor {

  constructor(private dialog: MatDialog) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        if (!err.error?.detail) {
          throw err;
        }

        switch (err.status) {
          case 400:
            break;
          case 404:
            break;
          case 409:
            return this.dialog.open(AlertDialogComponent, {
              width: '400px',
              disableClose: true,
              data: new AlertDialogModel({
                title: 'Error',
                message: err.error.detail,
                type: AlertDialogType.Error
              })
            }).afterClosed();
          default:
            break;
        }

        return throwError(() => new Error('No controlado'))
      }));
  }
}
