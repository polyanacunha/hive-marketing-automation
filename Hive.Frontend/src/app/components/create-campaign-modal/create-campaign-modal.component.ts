import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-create-campaign-modal',
  standalone: true,
  imports: [CommonModule, FormsModule, SidebarComponent, NavbarComponent],
  templateUrl: './create-campaign-modal.component.html',
  styleUrls: ['./create-campaign-modal.component.css']
})
export class CreateCampaignModalComponent {
  @Output() close = new EventEmitter<void>();
  sidebarOpen = false;

  campaign = {
    name: '',
    description: '',
    objective: '',
    ageMin: 18,
    ageMax: 65,
    interests: '',
    startDate: '',
    endDate: '',
    budget: 0
  };

  objectives = [
    'reconhecimento', 'trafego', 'engajamento', 'leads', 'vendas', 'promocao-app'
  ];

  selectObjective(value: string) {
    this.campaign.objective = value;
  }

  onClose() {
    this.close.emit();
  }

  onCreate() {
    console.log('Criar campanha', this.campaign);
    this.onClose();
  }

  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
  }

  closeSidebar() {
    this.sidebarOpen = false;
  }
}
