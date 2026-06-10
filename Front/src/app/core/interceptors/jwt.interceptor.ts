import {
  HttpInterceptorFn,
  HttpErrorResponse
} from '@angular/common/http';

import { inject } from '@angular/core';
import { Router } from '@angular/router';

import {
  catchError,
  throwError
} from 'rxjs';

import Swal from 'sweetalert2';

let sessionExpired = false;

export const jwtInterceptor: HttpInterceptorFn = (
  req,
  next
) => {

  const router = inject(Router);

  const token = localStorage.getItem('token');

  if (token) {

    req = req.clone({

      setHeaders: {

        Authorization: `Bearer ${token}`

      }

    });

  }

  return next(req).pipe(

    catchError((error: HttpErrorResponse) => {

      if (
        error.status === 401 &&
        !sessionExpired
      ) {

        sessionExpired = true;

        Swal.fire({
          icon: 'warning',
          title: 'Sesión expirada',
          text: 'Por favor inicie sesión nuevamente',
          confirmButtonColor: '#fea419',
          allowOutsideClick: false
        }).then(() => {

          localStorage.clear();

          router.navigate(['/login']);

        });

        localStorage.clear();

        router.navigate(['/login']);

      }

      return throwError(() => error);

    })

  );

};
