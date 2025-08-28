import { Component } from '@angular/core';
import { DataService } from '../../services/data.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login.component',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  private tokenKey = 'jwt_token';

  constructor(
    private dataService: DataService,
    private authService: AuthService,
    private router: Router
  ) {}

  login(): void {
    let loginObject = { Username: this.username, Password: this.password };
    this.dataService.postData('User/login', loginObject).subscribe({
      next: (response) => {
        // save token
        localStorage.setItem(this.tokenKey, response.token);
        // set username
        this.authService.setUser(this.username);
        // route to home
        this.router.navigate(['home']);
      },
      error: (err) => {
        console.error('err: ', err);
        alert(err.error);
      },
    });
  }
}
