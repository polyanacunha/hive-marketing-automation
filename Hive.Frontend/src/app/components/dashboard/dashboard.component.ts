import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CampaignsTableComponent } from '../campaigns-table/campaigns-table.component';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule, CampaignsTableComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {

}
