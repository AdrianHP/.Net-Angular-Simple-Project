import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountModule } from './account/account.module';
import { ProjectRoutes } from './project-routing';
import { LoginComponent } from './account/components/login/login.component';
import { AppComponent } from './app.component';
import { authGuard } from './utils/auth.guard';
import { HomeComponent } from './project/home/home/home.component';

const routes: Routes = [
  {
    path: 'account',
    loadChildren: () =>
      import('./account/account.module').then((m) => m.AccountModule),
  },
  {
    path: 'task',
    loadChildren: () =>import('./project/tasks/tasks.module').then((m) => m.TasksModule),
    canActivate: [authGuard]
  },
  {
    path: '',
    redirectTo:'/home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent ,
    canActivate: [authGuard],
  },

];
@NgModule({
  imports: [ RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
