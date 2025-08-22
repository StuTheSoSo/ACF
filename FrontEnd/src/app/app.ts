import { Component, OnInit, signal } from '@angular/core';
import { DataService } from './services/data.service.ts';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App implements OnInit{
  protected readonly title = signal('FrontEnd');

  constructor(private dataService: DataService){}

  ngOnInit(): void {
    console.log('ngOnInit');
    this.dataService.getData('User').subscribe({
      next: (response) => {
        console.log('response: ', response);
      },
      error: (err) => {
        console.error('err: ', err);
      }
    })
  }
}
