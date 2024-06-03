import {CanActivateFn, Router} from '@angular/router'
import {inject} from '@angular/core'
import {AuthService} from '../services/auth.service'
import {map} from 'rxjs'

export const authGuard: CanActivateFn = (route, state) => {
	const authService = inject(AuthService)
	const router = inject(Router)
	return authService.isAuth().pipe(
		map(isAuthenticated => {
			const isLoginPage = state.url.includes('auth')
			if (isAuthenticated && isLoginPage) {
				// Если пользователь авторизован и пытается попасть на страницу логина,
				// редиректим на аккаунт
				return router.createUrlTree(['/account'])
			} else if (!isAuthenticated && !isLoginPage) {
				// Если пользователь не авторизован и пытается попасть на страницу
				// аккаунта, редиректим на логин
				return router.createUrlTree(['/auth'])
			} else {
				// Иначе разрешаем доступ
				return isAuthenticated || isLoginPage
			}
		})
	)
}
