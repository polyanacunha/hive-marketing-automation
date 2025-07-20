import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CampaignsTableComponent } from '../campaigns-table/campaigns-table.component';
import { CampaignObjectiveModalComponent } from '../campaign-objective-modal.component';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, CampaignsTableComponent, CampaignObjectiveModalComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  showCampaignModal = false;

  openCampaignModal() {
    this.showCampaignModal = true;
  }

  closeCampaignModal() {
    this.showCampaignModal = false;
  }
}
