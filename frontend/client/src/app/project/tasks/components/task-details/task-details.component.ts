import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ITask } from '../../interfaces/task';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.scss']
})
export class TaskDetailsComponent {
task:ITask;
constructor( public dialogRef: MatDialogRef<TaskDetailsComponent>,
  @Inject(MAT_DIALOG_DATA) public data: any) {
  this.task = data.task;
  console.log(this.task)
}
}
