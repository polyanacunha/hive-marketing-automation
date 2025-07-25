import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { CampaignService } from '../../services/campaign/campaing.service'; // Import the service
import { CreateCampaignParams } from '../../services/campaign/campaing.service'; // Import the interface
import { CampaingDTO } from '../../models/campaing.dto'; // Correct import for CampaingDTO
import { AdCreationComponent } from '../ad-creation/ad-creation.component'; // Import AdCreationComponent

interface SelectedImages {
  images: string[];
}

@Component({
  selector: 'app-create-campaign-modal',
  standalone: true,
  imports: [CommonModule, FormsModule, SidebarComponent, NavbarComponent, AdCreationComponent], // Add AdCreationComponent to imports
  templateUrl: './create-campaign-modal.component.html',
  styleUrls: ['./create-campaign-modal.component.css']
})
export class CreateCampaignModalComponent {
  @Output() close = new EventEmitter<void>();
  sidebarOpen = false;
  selectedImages: SelectedImages = { images: [] };
  isAdCreationModalVisible = false;


  campaign = {
    name: '',
    description: '',
    productDescription: '',
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
    'leads': 1,
    'vendas': 5,
    'promocao-app': 6
  };

  constructor(private campaignService: CampaignService) {}

  selectObjective(value: string) {
    this.campaign.objective = value;
    this.campaign.objectiveCampaignId = this.objectiveIdMap[value] || 0; // Set the ID or default to 0
    console.log('Objective selected:', value, 'ID:', this.campaign.objectiveCampaignId); 
  }

  onClose() {
    this.close.emit();
  }

  saveCampaignData() {
    const initialDateISO = new Date(this.campaign.startDate).toISOString();
    const endDateISO = new Date(this.campaign.endDate).toISOString();

    const campaignData: CreateCampaignParams = {
      campaignName: this.campaign.name,
      budget: this.campaign.budget,
      initialDate: initialDateISO,
      endDate: endDateISO,
      objectiveCampaignId: this.campaign.objectiveCampaignId,
      productDescription: this.campaign.productDescription
    };

    console.log('Campaign data being sent:', campaignData);

    this.campaignService.createCampaign(campaignData).subscribe({
      next: (response: CampaingDTO) => {
        console.log('Campaign created successfully', response);
        this.onClose();
      },
      error: (error: any) => {
        console.error('Error creating campaign', error);
      }
    });
  }

  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
  }

  closeSidebar() {
    this.sidebarOpen = false;
  }

  createAds(): void {
    const campaignData: SelectedImages = {
      images: this.selectedImages.images,
    };

    this.campaignService.createAds([campaignData]).subscribe({
      next: (response) => {
        console.log('Ads created successfully', response);
      },
      error: (error) => {
        console.error('Error creating ads', error);
      }
    });
  }

  saveCampaignDataAndCreateAds() {
    this.saveCampaignData();
    this.createAds();
  }

  toggleAdCreationModal(): void {
    console.log('Toggle Ad Creation Modal called. Current state:', this.isAdCreationModalVisible);
    this.isAdCreationModalVisible = !this.isAdCreationModalVisible;
    console.log('New state:', this.isAdCreationModalVisible);
  }
}
