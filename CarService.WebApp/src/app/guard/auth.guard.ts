import { inject } from '@angular/core'
import { CanActivateFn, Router } from '@angular/router'
import { map } from 'rxjs'
import { AuthService } from '../services/auth.service'

export const authGuard: CanActivateFn = (route, state) => {
	const authService = inject(AuthService)
	const router = inject(Router)
	return authService.getRoleId().pipe(
		map(roleid => {
			const isAuthenticated = roleid != -1
			const isLoginPage = state.url.includes('auth')
			const requiredRoles = route.data['roles']
			// Получаем требуемые роли из данных маршрута

			if (isAuthenticated && isLoginPage) {
				// Если пользователь авторизован
				// и пытается попасть на страницу логина,
				// редиректим на аккаунт
				return router.createUrlTree(['/account'])
			} else if (!isAuthenticated && !isLoginPage) {
				// Если пользователь не авторизован и пытается попасть на страницу
				// аккаунта, редиректим на логин
				return router.createUrlTree(['/auth'])
			} else if (isAuthenticated && !isLoginPage) {
				// Если пользователь авторизован и
				// пытается попасть на защищенную страницу,
				// проверяем его роли
				const hasAccess = requiredRoles
					? requiredRoles.some((role: any) => roleid == role)
					: true
				if (!hasAccess) {
					// Если у пользователя нет доступа,
					// редиректим на страницу "доступ запрещен"
					return router.createUrlTree(['/account'])
				}
			}
			// Иначе разрешаем доступ
			return isAuthenticated || isLoginPage
		})
	)
}
