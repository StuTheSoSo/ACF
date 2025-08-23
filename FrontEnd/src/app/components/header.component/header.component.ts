// header.component.ts
import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common'; // For *ngIf

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule], // Import CommonModule for *ngIf
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  constructor(public authService: AuthService) {}
}