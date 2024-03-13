import { Component, OnInit } from '@angular/core';
import { StorageService } from 'src/app/account/services/storage.service';
import { IUser } from 'src/app/account/interfaces/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {

  user:IUser;
  constructor(private storage: StorageService) {}


  ngOnInit(): void {
    this.user = this.storage.getUser() as IUser;
  }

}
