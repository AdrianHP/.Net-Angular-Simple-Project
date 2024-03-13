import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, tap, throwError } from 'rxjs';
import { IUser } from 'src/app/account/interfaces/user';
import { Router } from '@angular/router';
import { StorageService } from 'src/app/account/services/storage.service';
import { ITask } from '../interfaces/task';

const AUTH_API = 'https://localhost:44390/api/Task/';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  //
  constructor(
    private http: HttpClient,
    private storageService: StorageService,
    private router: Router
  ) {}

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + String(this.storageService.getToken()),
    }),
  };

  errorMesage = '';


  getUserTasks() {
    return this.http
      .get<ITask[]>(AUTH_API + 'getUSerTasks', this.httpOptions)
      .pipe(
        catchError((error) => {
          console.log(error);
          return throwError(() => new Error(error.error));
        })
      );
  }

  addTasks(task: ITask) {
    task.assignedUserId = (this.storageService.getUser() as IUser).id;
    task.isCompleted = false;
    task.id = 0;
    this.http.post(AUTH_API, task,this.httpOptions).subscribe();
  }

  async editTask(task: ITask) {
    task.assignedUserId = (this.storageService.getUser() as IUser).id;
    this.http.put(AUTH_API+ task.id, task,this.httpOptions).subscribe();
  }

  deleteTask(id: string) {
    try {
      this.http.delete(AUTH_API + id, this.httpOptions).subscribe();
    } catch (error) {}
  }
}
