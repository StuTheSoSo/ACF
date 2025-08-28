import { Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common'; // For *ngIf
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  isOpen = false;

  // Navigation items
  navItems = [
    { name: 'Dashboard', route: 'home', role: 'Admin, Auditor, Officer' },
    { name: 'Clients', route: 'newclient', role: 'Admin, Officer' },
    { name: 'Cases', route: 'newcase', role: 'Admin, Officer' },
    { name: 'My Cases', route: 'mycases', role: 'Officer' },
    { name: 'Reports', route: 'reports', role: 'Admin, Auditor' },
    { name: 'Admin', route: 'admin', role: 'Admin' },
  ];

  constructor(public authService: AuthService) {}

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

  ngOnInit(): void {
    let userRole = this.authService.getUserRole();
    this.navItems = this.navItems.filter((item) =>
      item.role.includes(userRole)
    );
  }
}
