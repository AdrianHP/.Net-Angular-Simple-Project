import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountModule } from './account/account.module';
import { ProjectRoutes } from './project-routing';
import { LoginComponent } from './account/components/login/login.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  {
    path: 'account',
    loadChildren: () =>
      import('./account/account.module').then((m) => m.AccountModule),
  },
  {
    path: '',
    redirectTo:'/home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    component: AppComponent ,
  },
  // {
  //   path: '',
  //   redirectTo:'/home',
  //   pathMatch: 'full'
  // },

  // {
  //   path: 'home',
  //   component: HomeComponent ,
  //   canActivate: [authGuard],
  //   data: {roles: ['ROLE_ADMIN','ROLE_USER']}
  // },
  // {
  //   path: 'admin',
  //   component: AdminComponent,
  //   canActivate: [authGuard],
  //   data: {roles: ['ROLE_ADMIN']}
  // },
  // {
  //   path: 'forbidden',
  //   component: AccessDeniedComponent
  // },
  // {
  //   path: '**',
  //   redirectTo: '/home'
  // }
];
@NgModule({
  imports: [ RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
