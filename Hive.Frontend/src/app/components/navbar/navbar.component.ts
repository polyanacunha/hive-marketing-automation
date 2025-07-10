import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  @Output() sidebarToggle = new EventEmitter<void>();

  toggleSidebar() {
    this.sidebarToggle.emit();
  }

  openProfileMenu() {
    // Implementar menu de perfil (dropdown)
    console.log('Abrir menu de perfil');
  }
}
