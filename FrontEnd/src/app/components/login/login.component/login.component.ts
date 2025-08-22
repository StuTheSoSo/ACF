import { Component } from '@angular/core';
import { DataService } from '../../../services/data.service.ts';

@Component({
  selector: 'app-login.component',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  username: string = '';
  password: string = '';


  constructor(private dataService: DataService){}

  login(): void{
    let loginObject = {'Username': this.username, 'Password': this.password };
    this.dataService.postData('User/authenticate', loginObject).subscribe({
      next: (response) => {
        console.log('response: ', response);
      },
      error: (err) => {
        console.error('err: ', err);
      }
    })
  }
}
