import { Routes } from '@angular/router';
import { AuthGuard } from './guard/auth-guard';

import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { NewCaseComponent } from './components/new-case/new-case.component';
import { NewClientComponent } from './components/new-client/new-client.component';
import { ReportsComponent } from './components/reports/reports.component';
import { AdminComponent } from './components/admin/admin.component';


export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [ AuthGuard ] },
  { path: 'newcase', component: NewCaseComponent, canActivate: [ AuthGuard ] },
  { path: 'newclient', component: NewClientComponent, canActivate: [ AuthGuard ] },
  { path: 'register', component: RegisterComponent , canActivate: [ AuthGuard ]},
  { path: 'reports', component: ReportsComponent , canActivate: [ AuthGuard ]},
  { path: 'admin', component: AdminComponent , canActivate: [ AuthGuard ]},
  { path: '**', redirectTo: 'login', pathMatch: 'full' } 
];