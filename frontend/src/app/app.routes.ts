import { Routes } from '@angular/router';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { SearchPageComponent } from './pages/search-page/search-page.component';
import { canActivateAuth, canActivateGuest, canActivateRole } from './auth/access.guard';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { AccessDeniedComponent } from './pages/access-denied/access-denied.component';
import { AdminLayoutComponent } from './common-ui/admin-layout/admin-layout.component';
import { CreateKnifePageComponent } from './pages/admin/create-knife-page/create-knife-page.component';
import { AdminPanelComponent } from './pages/admin/admin-panel/admin-panel.component';
import { KnifePageComponent } from './pages/knife-page/knife-page.component';
import { EditKnifePageComponent } from './pages/admin/edit-knife-page/edit-knife-page.component';
import { ProfilePageComponent } from './pages/profile/profile-page/profile-page.component';
import { ProfileLayoutComponent } from './common-ui/profile-layout/profile-layout.component';
import { FavoritePageComponent } from './pages/profile/favorite-page/favorite-page.component';
import { EditRolesPageComponent } from './pages/admin/edit-roles-page/edit-roles-page.component';
import { EditCategoryPageComponent } from './pages/admin/edit-category-page/edit-category-page.component';

export const routes: Routes = [

  {path: '', component: MainPageComponent},
  {path: 'search', component: SearchPageComponent},
  {path: 'knife/:id', component: KnifePageComponent},
  {path: 'login', component: LoginPageComponent, canActivate: [canActivateGuest]},
  {path: 'register', component: RegisterPageComponent, canActivate: [canActivateGuest]},
  {path: 'profile', component: ProfileLayoutComponent, children: [
    {path: 'my-profile', component: ProfilePageComponent},
    {path: 'favorite', component: FavoritePageComponent}
    ], canActivate: [canActivateAuth]
  },
  {path: 'admin', component: AdminLayoutComponent, children: [
    {path: 'admin-panel', component: AdminPanelComponent},
    {path: 'create-knife', component: CreateKnifePageComponent},
    {path: 'edit-knife/:id', component: EditKnifePageComponent },
    {path: 'edit-category-page', component: EditCategoryPageComponent },
    {path: 'edit-roles', component: EditRolesPageComponent, canActivate: [canActivateRole(['Admin'])]},
    ], canActivate: [canActivateAuth, canActivateRole(['Manager'])]
  },
  {path: 'access-denied', component: AccessDeniedComponent }
];
