import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CampaignService } from '../services/campaign/campaing.service';

@Component({
  selector: 'app-campaign-objective-modal',
  imports: [CommonModule, FormsModule],
  templateUrl: './campaign-objective-modal.component.html',
  styleUrls: ['./campaign-objective-modal.component.css']
})
export class CampaignObjectiveModalComponent {
  @Output() closeModal = new EventEmitter<void>();

  objectives: any;

  selectedObjective: string | null = null;

  descriptionProduct: string = '';

  constructor(private router: Router, private campaignService: CampaignService) {
    this.loadObjectives();
  }

  close() {
    this.closeModal.emit();
  }

  openCreateCampaignModal() {
    this.router.navigate(['/create-campaign']);
  }

  loadObjectives() {
    this.campaignService.getCampaignObjectives().subscribe({
      next: (objectives) => {
        this.objectives = objectives;
      },
      error: (error) => {
        console.error('Error loading objectives', error);
      },
      complete: () => {
        console.log('Objectives loaded successfully');
      }
    });
  }
}