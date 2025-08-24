import { Routes } from '@angular/router';
import { AuthGuard } from './guard/auth-guard';

import { HomeComponent } from './components/home.component/home.component'; 
import { LoginComponent } from './components/login/login.component/login.component';
import { RegisterComponent } from './components/register.component/register.component';
import { NewCaseComponent } from './components/new-case.component/new-case.component';
import { NewClientComponent } from './components/new-client.component/new-client.component';


export const routes: Routes = [
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] }, 
  { path: 'newcase', component: NewCaseComponent, canActivate: [AuthGuard ]},
  { path: 'newclient', component: NewClientComponent, canActivate: [AuthGuard ]},
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' } // Wildcard for 404-like handling
];