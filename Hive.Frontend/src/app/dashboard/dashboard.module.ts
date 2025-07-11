import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DashboardComponent } from '../components/dashboard/dashboard.component';
import { CampaignsTableComponent } from '../components/campaigns-table/campaigns-table.component';
import { CampaignObjectiveModalComponent } from '../components/campaign-objective-modal.component';
import { CreateCampaignModalComponent } from '../components/create-campaign-modal/create-campaign-modal.component';

@NgModule({
  declarations: [
    DashboardComponent,
    CampaignsTableComponent,
    CampaignObjectiveModalComponent,
    CreateCampaignModalComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    DashboardComponent
  ]
})
export class DashboardModule { }
