import { Routes } from '@angular/router';
import { HomeComponent } from './components/login/home.component/home.component'; 
import { LoginComponent } from './components/login/login.component/login.component';
import { AuthGuard } from './guard/auth-guard';

export const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] }, 
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' } // Wildcard for 404-like handling
];