import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomValidators } from 'src/app/utils/custom-validators';
import { AuthService } from '../../services/auth.service';
import { BehaviorSubject, Subject, tap } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm = new FormGroup(
    {
      email: new FormControl(null, [Validators.required, Validators.email]),
      userName: new FormControl(null, [Validators.required]),
      firstName: new FormControl(null, [Validators.required]),
      lastName: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [
        Validators.required,
        Validators.pattern(
          /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/
        ),
      ]),
      passwordConfirm: new FormControl(null, [Validators.required]),
    },
    {
      validators: [
        CustomValidators.strongPassword,
        CustomValidators.passwordsMatching,
      ],
    }
  );

  errorMessage!: string;

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {}

  register() {
    if (!this.registerForm.valid) {
      return;
    }
    this.authService.register(this.registerForm.value).subscribe({
      next: () => {
        this.router.navigate(['account/login']);
      },
      error: (err) => {
        this.errorMessage = err.error;
      },
    });
  }
}
