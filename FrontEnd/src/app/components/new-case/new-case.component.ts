import { Component, OnInit } from '@angular/core';
import { Case } from '../../models/case';
import { DataService } from '../../services/data.service';
import { Client } from '../../models/client';
import { User } from '../../models/user';

@Component({
  selector: 'app-new-case.component',
  standalone: false,
  templateUrl: './new-case.component.html',
  styleUrls: ['./new-case.component.css']
})
export class NewCaseComponent implements OnInit {
  newCase: Case = new Case();
  currentCases: Case[] = [];
  currentClients: Client[] = [];
  currentOfficers: User[] = [];

  // HARD-CODING STATUS' HERE BUT THE MUST BE DATABASE OBJECTS
  statusValues = ['Active', 'Inactive'];
  regionValues = ['North', 'South', 'East', 'West'];

  constructor(private dataService: DataService) { }

  ngOnInit(): void {
    this.retrieveCases();
    this.retrieveClients();
    this.retrieveOfficers();
    this.newCase.status = this.statusValues[0];
    this.newCase.region = this.regionValues[0];
  }

  retrieveCases(): void {
    this.dataService.getData('Case').subscribe({
      next: (response) => {
        this.currentCases = response;
      },
      error: (err) => {
        console.error('err: ', err);
      }
    });
  }

  retrieveClients() {
    this.dataService.getData('Client').subscribe({
      next: (response) => {
        this.currentClients = response;
        this.newCase.clientId = this.currentClients[0].clientId;
      },
      error: (err) => {
        console.error('err: ', err);
      }
    });
  }

  retrieveOfficers() {
    this.dataService.getData('User/getOfficers').subscribe({
      next: (response) => {
        this.currentOfficers = response;
        this.newCase.officerId = this.currentOfficers[0].officerId;
      },
      error: (err) => {
        console.error('err: ', err);
      }
    });
  }

  clear() {
    this.newCase = new Case();
  }

  addCase() {
    this.newCase.caseId = crypto.randomUUID();
    this.dataService.postData('Case', this.newCase).subscribe({
      next: (response) => {
        this.newCase = new Case();
        this.retrieveCases();
      },
      error: (err) => {
        console.error('err: ', err);
        alert(err.error);
      }
    });
  }

  clientUpdated(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.newCase.clientId = selectElement.value;
  }

  officerUpdated(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.newCase.officerId = selectElement.value;
  }

  statusUpdated(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.newCase.status = selectElement.value;
  }

  regionUpdated(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.newCase.region = selectElement.value;
  }
}
