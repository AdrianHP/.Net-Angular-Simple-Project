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

  loginForm: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required]),
  });

  constructor(
    private authService: AuthService,
    private router: Router,
    private storageService: StorageService
  ) {}

  ngOnInit() {
    var user = this.storageService.getUser();
    if (user) {
      this.router.navigate(['home']);
    }
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
        this.router.navigate(['home']);
      },
      error: (err) => {
        this.errorMessage = err;
      },
    });
  }

  protected readonly console = console;
}
