import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { TaskService } from '../../services/task.service';
import {
  MatColumnDef,
  MatTable,
  MatTableDataSource,
} from '@angular/material/table';
import { ITask } from '../../interfaces/task';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { TaskFormComponent } from '../task-form/task-form.component';
import { BehaviorSubject, Subscription } from 'rxjs';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss'],
})
export class TasksComponent implements OnInit {
  errorMessage = '';

  public columns: MatColumnDef;
  public tasks: ITask[];
  public displayedColumns: any[] = [
    {
      field: 'title',
      name: 'Title',
    },
    {
      field: 'description',
      name: 'Description',
    },
    {
      field: 'dueDate',
      name: 'Due Date',
      pipe: 'date',
    },
    {
      field: 'isCompleted',
      name: 'Is Completed',
    },
  ];

  public columnsToDisplay: string[] = [
    'title',
    'description',
    'dueDate',
    'isCompleted',
    'actions',
  ];

  public columnsFilters = {};

  public dataSource: MatTableDataSource<ITask>;
  private serviceSubscribe: Subscription;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('grid', { static: false }) public grid: MatTable<ITask>;

  constructor(
    private taskService: TaskService,
    public dialog: MatDialog,
    private cd: ChangeDetectorRef
  ) {
    this.dataSource = new MatTableDataSource<ITask>();
  }
  ngOnInit(): void {
    this.fetchTasks();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  fetchTasks() {
    this.taskService.getUserTasks().subscribe({
      next: (userData:any) => {
        this.dataSource.data = userData;

      },
      error: (err:any) => {
        this.errorMessage = err;
      },
    });
  }

  addTask() {
    var dialogData = {
      isEdit: false,
    };
    const dialogRef = this.dialog.open(TaskFormComponent, {
      data: dialogData,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        console.log(result);
        this.taskService.addTasks(result);
      }
    });
  }

  editTask(data: ITask) {
    var dialogData = {
      isEdit: true,
      task: data,
    };
    const dialogRef = this.dialog.open(TaskFormComponent, {
      data: dialogData,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.taskService.editTask(result);
      }
    });
    this.fetchTasks();
    this.cd.detectChanges();
  }

  deleteTask(id: any) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent);

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        console.log(id);
        this.taskService.deleteTask(id);
        this.dataSource.data.filter((x) => x.id == id);
      }
    });
    this.fetchTasks();
    this.cd.detectChanges();
  }
}
