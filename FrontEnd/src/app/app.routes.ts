import { Routes } from '@angular/router';
import { HomeComponent } from './components/login/home.component/home.component'; 
import { LoginComponent } from './components/login/login.component/login.component';

export const routes: Routes = [
  { path: '', component: HomeComponent }, 
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' } // Wildcard for 404-like handling
];