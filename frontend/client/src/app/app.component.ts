import { Component } from '@angular/core';
import { TaskService } from './project/tasks/services/task.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'client';

  /**
   *
   */
  constructor() {
    ;
  }
  ngOnInit(): void {

  }
}
