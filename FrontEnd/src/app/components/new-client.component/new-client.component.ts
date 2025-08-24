import { Component } from '@angular/core';
import { Client } from '../../models/client';

@Component({
  selector: 'app-new-client.component',
  standalone: false,
  templateUrl: './new-client.component.html',
  styleUrl: './new-client.component.css'
})
export class NewClientComponent {
  newClient: Client = new Client();


  clear():void {
    this.newClient = new Client();
  }

  addClient():void{}
}
