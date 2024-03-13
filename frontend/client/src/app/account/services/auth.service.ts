import { EventEmitter, Injectable, Output } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, flatMap, tap, throwError } from 'rxjs';
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
  constructor(
    private http: HttpClient,
    private storageService: StorageService,
    private router: Router
  ) {}

  errorMesage = '';
  @Output() logChange: EventEmitter<any> = new EventEmitter();

  login(loginData: ILogin): Observable<any> {
    return this.http.post<any>(AUTH_API + 'login', loginData).pipe(
      catchError((error) => {
        return throwError(() => new Error(error.error));
      }),
      tap((userData) => {
        const token = userData.token;
        const authResult = userData.loggedUser;
        const extractedUser: IUser = authResult.loggedUser;
        this.storageService.saveToken(token);
        this.storageService.saveUser(extractedUser);
        this.logChange.emit('login');
      })
    );
  }
  autoLogin() {
    const userData = this.storageService.getUser();
    if (!userData) {
      return;
    }
  }

  register(registerData: any): Observable<any> {
    try {
      return this.http.post(AUTH_API + 'register', registerData, httpOptions);
    } catch (error: any) {
      return throwError(() => new Error(error.error));
    }
  }

  logout() {
    return this.http.post(AUTH_API + 'logout', {}, httpOptions).subscribe({
      next: () => {
        this.storageService.clean();
        this.router.navigate(['account/login']);
        this.logChange.emit('logout');
      },
    });
  }

}
