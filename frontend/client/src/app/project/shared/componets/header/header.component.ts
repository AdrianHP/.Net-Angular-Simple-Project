import { Component, OnInit } from '@angular/core';
import { StorageService } from 'src/app/account/services/storage.service';
import { AuthService } from 'src/app/account/services/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  isLoged: boolean;
  constructor(
    private storage: StorageService,
    private authService: AuthService,
    private router: Router
  ) {
    authService.logChange.subscribe((name) => this.changeLog(name));
    this.isLoged = storage.isLoggedIn();
  }

  ngOnInit(): void {}

  changeLog(name: string) {
    if (name == 'login') this.isLoged = true;
    else this.isLoged = false;
  }
  logOut() {
    this.authService.logout();
  }

  logIn() {
    this.router.navigate(['account/login']);
  }

  register() {
    this.router.navigate(['account/register']);
  }
  goTask(){
    this.router.navigate(['task']);
  }
  goHome(){
    this.router.navigate(['home']);
  }
}
