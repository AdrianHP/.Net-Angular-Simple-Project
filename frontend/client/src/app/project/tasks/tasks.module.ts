import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
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
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDialog } from '@angular/material/dialog';
import { TaskService } from './services/task.service';
import { MatOptionModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {  MatSelectModule } from '@angular/material/select';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
@NgModule({
  declarations: [
    TasksComponent,
    TaskFormComponent,
    TaskDetailsComponent,
    ConfirmationDialogComponent,
  ],
  imports: [
    CommonModule,
    TaskRoutingModule,
    MatTableModule,
    MatSelectModule,
    CdkTableModule,
    MatPaginatorModule,
    MatCardModule,
    MatSortModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSortModule,
    MatDialogModule,
    MatOptionModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSlideToggleModule,
    FormsModule,
    FlexLayoutModule,
  ],
  providers: [MatDialog, TaskService],
})
export class TasksModule {}
