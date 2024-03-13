import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TasksComponent } from './components/task-list/tasks.component';
import { TaskFormComponent } from './components/task-form/task-form.component';
import { TaskDetailsComponent } from './components/task-details/task-details.component';
import { TaskRoutingModule } from './task-routing.module';
import { MatTableModule } from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CdkTableModule } from '@angular/cdk/table';
import { MatSortModule } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
@NgModule({
  declarations: [TasksComponent, TaskFormComponent, TaskDetailsComponent],
  imports: [
    CommonModule,
    TaskRoutingModule,
    MatTableModule,
    CdkTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSortModule,
  ],
})
export class TasksModule {}
