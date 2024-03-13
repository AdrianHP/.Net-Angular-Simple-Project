import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ITask } from '../../interfaces/task';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.scss']
})
export class TaskFormComponent implements OnInit{
  taskForm: FormGroup;
  isEdit:boolean;
  inputTask:ITask;
  isComplete = false;

  constructor( public dialogRef: MatDialogRef<TaskFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data:any) {
      this.isEdit = data.isEdit;

      console.log(data.task)

    this.taskForm = new FormGroup({
      "id":  new FormControl(),
      "assignedUserId": new FormControl(),
      "title":  new FormControl('', Validators.required),
      "dueDate": new FormControl('', Validators.required),
      "isCompleted": new FormControl('', Validators.required),
      "description": new FormControl('', Validators.required),
    });

    if (this.isEdit)
    {
      this.inputTask = data.task;
      this.taskForm.setValue(this.inputTask);
    }

  }

  ngOnInit(): void {

  }

  save(): void {
    let result:ITask = this.taskForm.value;
    result.isCompleted = this.isComplete;
    this.dialogRef.close(Object.assign(result));
  }
}


