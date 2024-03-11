import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { RouterModule } from '@angular/router';
import { AccountRoutingModule } from './account-routing.module';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card'
import { MatFormFieldModule} from '@angular/material/form-field'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    AccountRoutingModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,

  ],
  providers: [],
})
export class AccountModule {}
