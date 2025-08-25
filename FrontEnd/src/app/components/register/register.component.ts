import { Component, OnInit } from '@angular/core';
import { RegisterObject } from '../../models/register.object';
import { DataService } from '../../services/data.service';
import { Router } from '@angular/router';
import { Role } from '../../models/role';

@Component({
  selector: 'app-register.component',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  roles: Role[] = [];;
  selectedRoleId: string | null = null;
  registerObject: RegisterObject = new RegisterObject();
  confirmPassword: string = '';

  constructor(private dataService: DataService, private router: Router) { }

  ngOnInit(): void {
    // Get roles 
    this.dataService.getData('Role').subscribe({
      next: (response) => {
        this.roles = response;
        this.roles.sort((a, b) => {
          const nameA = a.roleName.toLowerCase();
          const nameB = b.roleName.toLowerCase();
          if (nameA < nameB) {
            return -1; // a comes before b
          }
          if (nameA > nameB) {
            return 1; // b comes before a
          }
          return 0; // names are equal
        })
      },
      error: (err) => {
        console.error('err: ', err);
      }
    });
  }

  onRoleChange(roleId: string) {
    this.registerObject.Role = roleId;
  }

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
