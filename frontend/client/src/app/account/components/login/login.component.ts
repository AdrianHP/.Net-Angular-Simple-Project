import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ILogin } from '../../interfaces/login';
import { IUser } from '../../interfaces/user';
import { StorageService } from '../../services/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
  errorMessage!: string;
  AuthUserSub!: Subscription;

  loginForm: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required]),
  });

  constructor(private authService: AuthService, private router: Router,private storageService: StorageService,) {}

  ngOnInit() {
    // this.router.navigate(['home']);
    // this.AuthUserSub = this.authService.AuthenticatedUser$.subscribe({
    //   next : user => {
    //     if(user) {
    //       this.router.navigate(['home']);
    //     }
    //   }
    // })
  }

  onSubmitLogin(formLogin: NgForm) {
    if (!formLogin.valid) {
      return;
    }
    let loginData: ILogin = {
      email: formLogin.value.email,
      password: formLogin.value.password,
    };

    this.authService.login(loginData).subscribe({
      next: (userData) => {
        this.router.navigate(['home']);
      },
      error: (err) => {
        this.errorMessage = err;
        console.log(err);
      },
    });
  }
  ngOnDestroy() {
    // this.AuthUserSub.unsubscribe();
  }

  login() {
    if (!this.loginForm.valid) {
      return;
    }
    this.authService.login(this.loginForm.value).subscribe({
      next: (userData) => {
        const token = userData.token;
        const authResult = userData.loggedUser;
        const extractedUser: IUser = authResult.loggedUser;
        this.storageService.saveToken(token);
        this.storageService.saveUser(extractedUser);
        this.console.log (userData)
        this.router.navigate(['home']);
      },
      error: (err) => {
        this.errorMessage = err;
        console.log(err);
      },
    });
  }

  protected readonly console = console;
}