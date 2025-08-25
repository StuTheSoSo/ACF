import { Routes } from '@angular/router';
import { AuthGuard } from './guard/auth-guard';

import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { NewCaseComponent } from './components/new-case/new-case.component';
import { NewClientComponent } from './components/new-client/new-client.component';


export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [ AuthGuard ] },
  { path: 'newcase', component: NewCaseComponent, canActivate: [ AuthGuard ] },
  { path: 'newclient', component: NewClientComponent, canActivate: [ AuthGuard ] },
  { path: 'register', component: RegisterComponent , canActivate: [ AuthGuard ]},
  { path: '**', redirectTo: '', pathMatch: 'full' } // Wildcard for 404-like handling
];