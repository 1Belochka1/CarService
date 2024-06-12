import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http'
import {inject} from '@angular/core'
import {ToastrService} from 'ngx-toastr'
import {catchError, throwError} from 'rxjs'

export const serverErrorInterceptor: HttpInterceptorFn = (req, next) => {
	const toastr = inject(ToastrService)

	return next(req).pipe(
		catchError((error: HttpErrorResponse) => {
			if (
				error.status === 500 ||
				error.status === 504 ||
				error.status === 501 ||
				error.status === 502 ||
				error.status === 503
			) {
				toastr.error(
					'На сервере произошла ошибка, просим прощения за неудобства',
					'Ошибка'
				)
			}
			if (error.status === 400) {
				toastr.error(
					error.error,
					'Ошибка'
				)
			}
			return throwError(error)
		})
	)
}
