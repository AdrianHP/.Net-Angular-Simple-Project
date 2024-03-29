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
import { TaskDetailsComponent } from '../task-details/task-details.component';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss'],
})
export class TasksComponent implements OnInit {
  errorMessage = '';
  taskStateFilter = 'all_inclusive';

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
      icon: 'check_circle',
    },
  ];

  public columnsToDisplay: string[] = [
    'title',
    'description',
    'dueDate',
    'isCompleted',
    'actions',
    'details'
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
    console.log(new Date());
    this.fetchTasks();
    this.dataSource.filterPredicate = this.filterData;
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  async fetchTasks() {
    await this.taskService
      .getUserTasks()
      .then((userData: any) => {
        this.dataSource.data = userData;
        this.cd.detectChanges();
      })
      .catch((err: any) => {
        this.errorMessage = err;
      });
  }

  addTask() {
    var dialogData = {
      isEdit: false,
    };
    const dialogRef = this.dialog.open(TaskFormComponent, {
      data: dialogData,
    });

    dialogRef.afterClosed().subscribe(async (result) => {
      if (result) {
        await this.taskService.addTasks(result).then(() => {
          this.fetchTasks();
        });
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
        this.taskService.editTask(result).then(async () => {
          await this.fetchTasks();
        });
      }
    });
  }

  taskDetails(data: ITask) {
    var dialogData = {
      task: data,
    };
    this.dialog.open(TaskDetailsComponent, {
      data: dialogData,
    });
  }

  deleteTask(id: any) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent);

    dialogRef.afterClosed().subscribe(async (result) => {
      if (result) {
        await this.taskService.deleteTask(id).then(() => {
          this.fetchTasks();
        });
      }
    });
  }

  filterData(data: ITask, filter: string): boolean {
    var result = true;

    return data.title.toLowerCase().includes(filter);
    return true;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }
}
