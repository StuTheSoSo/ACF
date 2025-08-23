import { Routes } from '@angular/router';
import { AuthGuard } from './guard/auth-guard';

import { HomeComponent } from './components/home.component/home.component'; 
import { LoginComponent } from './components/login/login.component/login.component';
import { RegisterComponent } from './components/register.component/register.component';


export const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] }, 
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' } // Wildcard for 404-like handling
];