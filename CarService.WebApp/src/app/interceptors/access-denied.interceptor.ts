import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http'
import {catchError, throwError} from 'rxjs'
import {inject} from '@angular/core'
import {ToastrService} from 'ngx-toastr'

export const accessDeniedInterceptor: HttpInterceptorFn = (req, next) => {
	const toastr = inject(ToastrService)

	return next(req).pipe(
		catchError((error: HttpErrorResponse) => {
			if (error.status === 403) {
				toastr.error('Доступ запрещен', 'Ошибка')
			}
			return throwError(error)
		})
	)
}
