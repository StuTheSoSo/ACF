import { Component } from '@angular/core';
import { DataService } from '../../services/data.service.ts';

@Component({
  selector: 'app-home.component',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(private dataService: DataService){}

  
}
