import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, tap, throwError } from 'rxjs';
import { IUser } from 'src/app/account/interfaces/user';
import { Router } from '@angular/router';
import { StorageService } from 'src/app/account/services/storage.service';
import { ITask } from '../interfaces/task';
import { IHttpResponsesHandler } from '../interfaces/http';

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

  protected handleRequest<TResult>(
    request: Observable<TResult>,
    responseHandler?: IHttpResponsesHandler): Promise<TResult> {

    const promise = request.toPromise() as Promise<TResult>;
    if (responseHandler) {
        return promise.then((response:any) => {
            if (responseHandler.handleSuccess) {
                responseHandler.handleSuccess();
            }
            return Promise.resolve(response);
        }).catch((errorResponse: HttpErrorResponse) => {
            if (responseHandler.handleError) {
                responseHandler.handleError(errorResponse);
            }
            return Promise.reject(errorResponse);
        });
    } else {
        return promise;
    }
}


  async getUserTasks():Promise<ITask[]> {
    return this.handleRequest<ITask[]>(
      this.http.get<ITask[]>(AUTH_API + 'getUSerTasks', this.httpOptions))
  }

  async addTasks(task: ITask) {
    task.assignedUserId = (this.storageService.getUser() as IUser).id;
    task.isCompleted = false;
    task.id = 0;
    return this.handleRequest<void>(
      this.http.post<void>(AUTH_API, task,this.httpOptions))
  }

  async editTask(task: ITask) {
    task.assignedUserId = (this.storageService.getUser() as IUser).id;
    return this.handleRequest<void>(
      this.http.put<void>(AUTH_API+ task.id, task,this.httpOptions))
  }

  async deleteTask(id: string) {
    try {
      return this.handleRequest<void>(
        this.http.delete<void>(AUTH_API + id, this.httpOptions))
    } catch (error) {}
  }
}
