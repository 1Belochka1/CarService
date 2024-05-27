import {CanActivateFn, Router} from '@angular/router'
import {inject} from '@angular/core'
import {AuthService} from '../services/auth.service'

export const authGuard: CanActivateFn = (route, state) => {
	// const authService = inject(AuthService)
	//
	// const router = inject(Router)
	//
	// console.log(state)
	//
	// authService.getByCookie()
	//
	// if (authService.user) {
	// 	if (state.url == '/auth') {
	// 		return router.createUrlTree(['account'])
	// 	}
	// 	return true
	// }
	//
	// if (state.url == '/auth') {
	// 	return true
	// }
	//
	// return router.createUrlTree(['auth'])

	return true
}
