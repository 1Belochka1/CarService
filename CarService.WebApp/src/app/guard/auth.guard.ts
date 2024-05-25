import {CanActivateFn, Router} from '@angular/router'
import {inject} from '@angular/core'
import {AuthService} from '../services/auth.service'

export const authGuard: CanActivateFn = (route, state) => {
	const authService = inject(AuthService)
	const router = inject(Router)
	console.log(authService)
	if (authService.user)
		return true

	return router.createUrlTree(['auth'])
}