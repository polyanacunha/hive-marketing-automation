import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { CampaingService } from '../../services/campaing.service'; // Import the service
import { CreateCampaignParams } from '../../services/campaing.service'; // Import the interface
import { CampaingDTO } from '../../models/campaing.dto'; // Correct import for CampaingDTO

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
    budget: 0,
    objectiveCampaignId: 0
  };

  objectives = [
    'reconhecimento', 'trafego', 'engajamento', 'leads', 'vendas', 'promocao-app'
  ];

  private objectiveIdMap: { [key: string]: number } = {
    'reconhecimento': 1,
    'trafego': 2,
    'engajamento': 3,
    'leads': 4,
    'vendas': 5,
    'promocao-app': 6
  };

  constructor(private campaingService: CampaingService) {} // Inject the service

  selectObjective(value: string) {
    this.campaign.objective = value;
    this.campaign.objectiveCampaignId = this.objectiveIdMap[value] || 0; // Set the ID or default to 0
    console.log('Objective selected:', value, 'ID:', this.campaign.objectiveCampaignId); // Log the selected objective and ID
  }

  onClose() {
    this.close.emit();
  }

  onCreate() {
    const campaignData: CreateCampaignParams = {
      campaignName: this.campaign.name,
      budget: this.campaign.budget,
      initialDate: this.campaign.startDate,
      endDate: this.campaign.endDate,
      objectiveCampaignId: this.campaign.objectiveCampaignId
    };

    console.log('Campaign data being sent:', campaignData);

    this.campaingService.create(campaignData).subscribe({
      next: (res: CampaingDTO) => {
        console.log('Campaign created successfully', res);
        this.onClose();
      },
      error: (err: any) => {
        console.error('Error creating campaign', err);
      }
    });
  }

  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
  }

  closeSidebar() {
    this.sidebarOpen = false;
  }
}
