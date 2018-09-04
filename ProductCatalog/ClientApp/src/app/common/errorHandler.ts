import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpErrorResponse
} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Router } from "@angular/router";
import { catchError } from "rxjs/internal/operators";

declare var toastr: any;

@Injectable()
export class ErrorHandler implements HttpInterceptor {

    constructor(private router: Router) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError((error, caught) => {
            this.handleError(error);
            return of(error);
        }) as any);
    }

    private handleError(error: HttpErrorResponse) {
        if (error.error) {
            console.log('handled http request error ' + JSON.stringify(error.error, null, 4));
            toastr.error(error.error.message);
        }
        else {
            console.log('Http request error');
            toastr.error("error during request");
        }
    }
}
