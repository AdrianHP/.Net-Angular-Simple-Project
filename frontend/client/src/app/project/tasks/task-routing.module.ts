import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TasksComponent } from './components/task-list/tasks.component';

const routes: Routes = [
  {
    path: '',
    component: TasksComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
})
export class TaskRoutingModule {}
