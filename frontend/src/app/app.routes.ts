import { Routes } from '@angular/router';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { SearchPageComponent } from './pages/search-page/search-page.component';
import { canActivateAuth, canActivateGuest, canActivateRole } from './auth/access.guard';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { AccessDeniedComponent } from './pages/access-denied/access-denied.component';
import { AdminLayoutComponent } from './common-ui/admin-layout/admin-layout.component';
import { CreateKnifePageComponent } from './pages/create-knife-page/create-knife-page.component';
import { AdminPanelComponent } from './pages/admin-panel/admin-panel.component';
import { KnifePageComponent } from './pages/knife-page/knife-page.component';
import { EditKnifePageComponent } from './pages/edit-knife-page/edit-knife-page.component';

export const routes: Routes = [

  {path: '', component: MainPageComponent},
  {path: 'search', component: SearchPageComponent},
  {path: 'knife/:id', component: KnifePageComponent},
  {path: 'login', component: LoginPageComponent, canActivate: [canActivateGuest]},
  {path: 'register', component: RegisterPageComponent, canActivate: [canActivateGuest]},
  {path: '', component: AdminLayoutComponent, children: [
    {path: 'admin-panel', component: AdminPanelComponent},
    {path: 'create-knife', component: CreateKnifePageComponent},
    {path: 'edit-knife/:id', component: EditKnifePageComponent},
    ], canActivate: [canActivateAuth, canActivateRole(['Admin'])]
  },
  {path: 'access-denied', component: AccessDeniedComponent }
];
