import { Component } from '@angular/core';
import { RegisterObject } from '../../models/register.object';
import { DataService } from '../../services/data.service.ts';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register.component',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  // username: string = '';
  // firstName: string = '';
  // lastName: string = '';
  // role: string = '';
  // password: string = '';
  registerObject: RegisterObject = new RegisterObject();
  confirmPassword: string = '';

  constructor(private dataService: DataService, private router: Router) { }


  register(): void {
    // HACK - DEMO ONLY. THIS WOULD NORMALLY INVOLVE A BETTER VALIDATION!
    if (this.registerObject.Password != this.confirmPassword) {
      alert('Password and Confirmation Password mismatch');
    }
    else {
      this.dataService.postData('User/register', this.registerObject).subscribe({
        next: (response) => {
          // route to home
          this.router.navigate(['login']);
        },
        error: (err) => {
          console.error('err: ', err);
        }
      });
    }
  }
}
