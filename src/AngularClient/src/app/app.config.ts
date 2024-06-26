import {ApplicationConfig, provideZoneChangeDetection} from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import {MAT_LUXON_DATE_ADAPTER_OPTIONS, provideLuxonDateAdapter} from '@angular/material-luxon-adapter';

import {MAT_FORM_FIELD_DEFAULT_OPTIONS} from "@angular/material/form-field";
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import {HttpErrorsInterceptor} from "./common/interceptors/http-errors.interceptor";
import {provideAnimationsAsync} from "@angular/platform-browser/animations/async";
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideLuxonDateAdapter(),
    provideHttpClient(
      withInterceptorsFromDi()
    ),
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'outline', floatLabel: 'always'} },
    { provide: MAT_LUXON_DATE_ADAPTER_OPTIONS, useValue: {useUtc: true}},
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorsInterceptor, multi: true }
  ]
};
