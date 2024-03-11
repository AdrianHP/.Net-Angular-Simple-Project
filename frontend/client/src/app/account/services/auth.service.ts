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

  login(loginData: ILogin): Observable<any> {
    return this.http.post<any>(AUTH_API + 'login', loginData, httpOptions).pipe(
      catchError((err) => {
        console.log(err);
        let errorMessage = 'An unknown error occurred!';
        if (err.error.message === 'Bad credentials') {
          errorMessage = 'The email address or password you entered is invalid';
        }
        return throwError(() => new Error(errorMessage));
      }),
      tap((response) => {
        const token = response.token;
        const authResult = response.loggedUser;
        const extractedUser: IUser = authResult.loggedUser;
        this.storageService.saveToken(token);
        this.storageService.saveUser(extractedUser);
        this.AuthenticatedUser$.next(extractedUser);
      })
    );
  }

  register(registerData: IUserRegistration): Observable<any> {
    return this.http.post(AUTH_API + 'register', registerData, httpOptions);
  }

  logout(): Observable<any> {
    return this.http.post(AUTH_API + 'logout', {}, httpOptions);
  }
}
