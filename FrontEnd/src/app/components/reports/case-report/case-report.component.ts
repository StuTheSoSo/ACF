import { Component, OnInit } from '@angular/core';
import { Client } from 'src/app/models/client';
import { User } from 'src/app/models/user';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-case-report',
  templateUrl: './case-report.component.html',
  styleUrls: ['./case-report.component.css'],
})
export class CaseReportComponent implements OnInit {
  officersWithCases: User[] = [];
  selectedOfficerId: string = '';
  clientsPerOfficer: Client[] = [];

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    // OnInit get distinct officers
    this.dataService.getData('Reports/officers').subscribe({
      next: (response) => {
        this.officersWithCases = response;
      },
      error: (err) => {
        console.error('err: ', err);
      },
    });
  }

  officerSelected(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.selectedOfficerId = selectElement.value;
    // Get all clients for this officer
    this.dataService
      .getData('Reports/clientsByOfficer/' + this.selectedOfficerId)
      .subscribe({
        next: (response) => {
          this.clientsPerOfficer = response;
        },
        error: (err) => {
          console.error('err: ', err);
        },
      });
  }
}
