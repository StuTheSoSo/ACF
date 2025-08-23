import { Component, HostListener  } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common'; // For *ngIf

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule], 
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
 constructor(public authService: AuthService) {}

 isOpen = false;

  // Navigation items
  navItems = [
    { name: 'Dashboard', route: '/home'},
    { name: 'New Case', route: '/users' },
    { name: 'Reports', route: '/settings' },
    { name: 'Admin', route: '/logout' }
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
