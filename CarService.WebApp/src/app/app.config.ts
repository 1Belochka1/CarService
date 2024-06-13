import { registerLocaleData } from '@angular/common'
import {provideHttpClient, withFetch, withInterceptors} from '@angular/common/http'
import localeRu from '@angular/common/locales/ru'
import { ApplicationConfig, LOCALE_ID } from '@angular/core'
import { provideNativeDateAdapter } from '@angular/material/core'
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async'
import { provideRouter, withInMemoryScrolling } from '@angular/router'
import { provideAngularSvgIcon } from 'angular-svg-icon'
import { routes } from './app.routes'
import { AuthService } from './services/auth.service'

import { provideToastr } from 'ngx-toastr'
import { register } from 'swiper/element'
import { accessDeniedInterceptor } from './interceptors/access-denied.interceptor'
import { serverErrorInterceptor } from './interceptors/server-error.interceptor'

register()

registerLocaleData(localeRu)

export const appConfig: ApplicationConfig = {
	providers: [
		AuthService,
		provideRouter(
			routes,
			withInMemoryScrolling({
				anchorScrolling: 'enabled',
				scrollPositionRestoration: 'top',
			})
		),
		provideAnimationsAsync(),
		provideAngularSvgIcon(),
		provideNativeDateAdapter(),
		provideToastr(),
		provideHttpClient(
withFetch(),
			withInterceptors([accessDeniedInterceptor, serverErrorInterceptor])
		),
		{
			provide: LOCALE_ID,
			useValue: 'ru',
		},
		provideAnimationsAsync(),
		provideAnimationsAsync(),
	],
}
