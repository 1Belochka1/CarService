import {provideHttpClient} from "@angular/common/http";
import {ApplicationConfig} from '@angular/core';
import {provideAnimationsAsync} from '@angular/platform-browser/animations/async';
import {provideRouter} from '@angular/router';
import {provideAngularSvgIcon} from "angular-svg-icon";
import {routes} from './app.routes';
import {UsersService} from "./services/users.service";

export const appConfig: ApplicationConfig = {
	providers: [UsersService, provideRouter(routes), provideAnimationsAsync(), provideAngularSvgIcon(), provideHttpClient()]
};
