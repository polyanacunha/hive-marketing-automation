import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { CampaignService } from '../../services/campaign/campaing.service'; // Import the service
import { CreateCampaignParams } from '../../services/campaign/campaing.service'; // Import the interface
import { CampaingDTO } from '../../models/campaing.dto'; // Correct import for CampaingDTO
import { AdCreationComponent } from '../ad-creation/ad-creation.component'; // Import AdCreationComponent
import { MediaService } from '../../services/media/media.service'; // Import MediaService
import { Observable } from 'rxjs'; // Import Observable
import { map } from 'rxjs/operators'; // Import map

interface SelectedImages {
  images: string[];
}

@Component({
  selector: 'app-create-campaign-modal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    SidebarComponent,
    NavbarComponent,
    AdCreationComponent,
  ], // Add AdCreationComponent to imports
  templateUrl: './create-campaign-modal.component.html',
  styleUrls: ['./create-campaign-modal.component.css'],
})
export class CreateCampaignModalComponent {
  @Output() close = new EventEmitter<void>();
  sidebarOpen = false;
  selectedImages: File[] = [];
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
    objectiveCampaignId: 0,
  };

  objectives = [
    'reconhecimento',
    'trafego',
    'engajamento',
    'leads',
    'vendas',
    'promocao-app',
  ];

  private objectiveIdMap: { [key: string]: number } = {
    reconhecimento: 1,
    trafego: 2,
    engajamento: 3,
    leads: 1,
    vendas: 5,
    'promocao-app': 6,
  };

  constructor(
    private campaignService: CampaignService,
    private router: Router,
    private mediaService: MediaService
  ) {}

  selectObjective(value: string) {
    this.campaign.objective = value;
    this.campaign.objectiveCampaignId = this.objectiveIdMap[value] || 0; // Set the ID or default to 0
    console.log(
      'Objective selected:',
      value,
      'ID:',
      this.campaign.objectiveCampaignId
    );
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
      productDescription: this.campaign.productDescription,
    };

    console.log('Campaign data being sent:', campaignData);

    this.campaignService.createCampaign(campaignData).subscribe({
      next: (response: CampaingDTO) => {
        console.log('Campaign created successfully', response);
        this.onClose();
      },
      error: (error: any) => {
        console.error('Error creating campaign', error);
      },
    });
  }

  toggleSidebar() {
    this.sidebarOpen = !this.sidebarOpen;
  }

  closeSidebar() {
    this.sidebarOpen = false;
  }

  createAds(): void {
    // Assume you have a method to upload images and get their IDs
    this.uploadImagesAndGetIds().subscribe((imageIds: number[]) => {
      const clientObservations = 'Your observations here'; // Replace with actual observations
      const inputImagesId = imageIds;

      this.campaignService
        .createAds(clientObservations, inputImagesId)
        .subscribe({
          next: (response) => {
            console.log('Ads created successfully', response);
          },
          error: (error) => {
            console.error('Error creating ads', error);
          },
        });
    });
  }
  //mudar a implementacao desse metodo para buscar as imagens do bucket
  uploadImagesAndGetIds(): Observable<number[]> {
    const formData = new FormData();
    this.selectedImages.forEach((file) => {
      formData.append('Files', file);
    });

    return this.mediaService
      .uploadImages(formData)
      .pipe(map((response) => response.imageIds));
  }

  saveCampaignDataAndCreateAds() {
    this.saveCampaignData();
    console.log('Creating ads with images:', this.selectedImages);
    this.createAds();
    console.log('Creating ads with images:', this.selectedImages);
    this.router.navigate(['/media-gallery']);
  }

  toggleAdCreationModal(): void {
    console.log(
      'Toggle Ad Creation Modal called. Current state:',
      this.isAdCreationModalVisible
    );
    this.isAdCreationModalVisible = !this.isAdCreationModalVisible;
    console.log('New state:', this.isAdCreationModalVisible);
  }

  onImagesSelected(files: File[]): void {
    this.selectedImages = files;
  }
}
