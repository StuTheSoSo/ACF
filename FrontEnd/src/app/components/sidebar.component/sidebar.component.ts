import { Component, HostListener  } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common'; // For *ngIf
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule], 
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
 constructor(public authService: AuthService) {}

 isOpen = false;

  // Navigation items
  navItems = [
    { name: 'Dashboard', route: 'home'},
    { name: 'Clients', route: 'newclient' },
    { name: 'Cases', route: 'newcase' },
    { name: 'My Cases', route: 'mycases'},
    { name: 'Reports', route: 'reports' },
    { name: 'Admin', route: 'logout' }
  ];

  // Toggle sidebar
  toggleSidebar() {
    this.isOpen = !this.isOpen;
  }

  // Close sidebar on mobile when clicking outside
  @HostListener('document:click', ['$event'])
  onDocumentClick(event: Event) {
    if (this.isOpen && window.innerWidth < 768) {
      const target = event.target as HTMLElement;
      if (!target.closest('.sidebar') && !target.closest('.toggle-btn')) {
        this.isOpen = false;
      }
    }
  }
}
