import {provideHttpClient} from '@angular/common/http'
import {ApplicationConfig, LOCALE_ID} from '@angular/core'
import {provideAnimationsAsync} from '@angular/platform-browser/animations/async'
import {provideRouter} from '@angular/router'
import {provideAngularSvgIcon} from 'angular-svg-icon'
import {routes} from './app.routes'
import {registerLocaleData} from '@angular/common'
import localeRu from '@angular/common/locales/ru'
import {AuthService} from './services/auth.service'
import {provideNativeDateAdapter} from '@angular/material/core'

import {register} from 'swiper/element'

register()

registerLocaleData(localeRu)

export const appConfig: ApplicationConfig = {
	providers: [AuthService,
		provideRouter(routes),
		provideAnimationsAsync(),
		provideAngularSvgIcon(),
		provideNativeDateAdapter(),
		provideHttpClient(), {
			provide: LOCALE_ID,
			useValue: 'ru'
		}]
}
