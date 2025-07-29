import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  @Input() isOpen: boolean = false;
  @Output() closeSidebarEvent = new EventEmitter<void>();

  closeSidebar() {
    this.closeSidebarEvent.emit();
  }

  logout() {
    // Implementar lógica de logout
    console.log('Logout realizado');
  }
}
