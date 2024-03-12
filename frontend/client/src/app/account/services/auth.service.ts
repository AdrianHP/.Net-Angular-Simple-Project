import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, tap, throwError } from 'rxjs';
import { ILogin } from '../interfaces/login';
import { IUserRegistration } from '../interfaces/userRegistration';
import { IUser } from '../interfaces/user';
import { IAuthresult } from '../interfaces/authResult';
import { Router } from '@angular/router';
import { StorageService } from './storage.service';

const AUTH_API = 'https://localhost:44390/api/account/';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  AuthenticatedUser$ = new BehaviorSubject<IUser | null>(null);
  constructor(
    private http: HttpClient,
    private storageService: StorageService,
    private router: Router
  ) {}

  errorMesage = "";



  login(loginData: ILogin): Observable<any> {
    return this.http.post<any>(AUTH_API + 'login', loginData).pipe(
      catchError((error) => {
        console.log(error);
        return throwError(() => new Error(error.error));
      }),
    );
  }

  register(registerData: any): Observable<any> {
    try {
      return this.http.post(AUTH_API + 'register', registerData, httpOptions);
    } catch (error: any) {
      return throwError(() => new Error(error.error));
    }
  }

  logout(): Observable<any> {
    return this.http.post(AUTH_API + 'logout', {}, httpOptions);
  }
}
