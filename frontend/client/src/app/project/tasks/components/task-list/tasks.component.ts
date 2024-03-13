import { Component, OnInit, ViewChild } from '@angular/core';
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

  public columnsToDisplay: string[] = ['title','description','dueDate','isCompleted', 'actions'];

  public columnsFilters = {};

  public dataSource: MatTableDataSource<ITask>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('grid', { static: false }) public grid: MatTable<ITask>;

  constructor(private taskService: TaskService) {
    this.dataSource = new MatTableDataSource<ITask>();
  }
  ngOnInit(): void {
    this.getTasks();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getTasks() {
    this.taskService.getUserTasks().subscribe({
      next: (userData) => {
        this.dataSource.data = userData;
        console.log(this.dataSource.data);
      },
      error: (err) => {
        this.errorMessage = err;
      },
    });
  }

  addTask() {}

  editTask(data: ITask) {}

  deleteTask(id: any) {}
}
