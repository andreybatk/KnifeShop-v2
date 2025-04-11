import { Routes } from '@angular/router';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { SearchPageComponent } from './pages/search-page/search-page.component';
import { LayoutComponent } from './common-ui/layout/layout.component';
import { canActivateAuth, canActivateGuest } from './auth/access.guard';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { MainPageComponent } from './pages/main-page/main-page.component';

export const routes: Routes = [

  {path: '', component: MainPageComponent},
  {path: 'search', component: SearchPageComponent},
  {path: 'login', component: LoginPageComponent, canActivate: [canActivateGuest]},
  {path: 'register', component: RegisterPageComponent, canActivate: [canActivateGuest]}
];
