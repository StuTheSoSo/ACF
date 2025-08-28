import { Component, OnInit } from '@angular/core';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-home.component',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  totalCases: number = 0;
  personalCases: number = 0;
  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.dataService.getData('Home').subscribe((x) => {
      this.totalCases = x.totalCases;
      this.personalCases = x.personalCases;
    });
  }
}
