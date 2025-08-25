import { Component, OnInit } from '@angular/core';
import { Client } from '../../models/client';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-new-client.component',
  standalone: false,
  templateUrl: './new-client.component.html',
  styleUrls: ['./new-client.component.css']
})
export class NewClientComponent implements OnInit {
  newClient: Client = new Client();
  currentClients: Client[] = [];

  constructor(private dataService: DataService) { }
  ngOnInit(): void {
    this.retrieveClients();
  }

  retrieveClients() {
    this.dataService.getData('Client').subscribe({
      next: (response) => {
        this.currentClients = response;
      },
      error: (err) => {
        console.error('err: ', err);
      }
    });
  }


  clear(): void {
    this.newClient = new Client();
  }

  addClient(): void {
    this.newClient.clientId = crypto.randomUUID();
    this.dataService.postData('Client', this.newClient).subscribe({
      next: (response) => {
        this.newClient = new Client();
        this.retrieveClients();
      },
      error: (err) => {
        console.error('err: ', err);
        alert(err.error);
      }
    });
  }
}
