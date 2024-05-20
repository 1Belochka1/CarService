import {provideHttpClient} from '@angular/common/http'
import {ApplicationConfig, LOCALE_ID} from '@angular/core'
import {provideAnimationsAsync} from '@angular/platform-browser/animations/async'
import {provideRouter} from '@angular/router'
import {provideAngularSvgIcon} from 'angular-svg-icon'
import {routes} from './app.routes'
import {UsersService} from './services/users.service'
import {registerLocaleData} from '@angular/common'
import localeRu from '@angular/common/locales/ru'

registerLocaleData(localeRu);

export const appConfig: ApplicationConfig = {
	providers: [UsersService, provideRouter(routes), provideAnimationsAsync(), provideAngularSvgIcon(), provideHttpClient(), {
		provide: LOCALE_ID,
		useValue: 'ru'
	}]
}
