import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-campaign-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-campaign-modal.component.html',
  styleUrls: ['./create-campaign-modal.component.css']
})
export class CreateCampaignModalComponent {
  @Output() close = new EventEmitter<void>();

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
    // enviar dados ou chamar serviço
    console.log('Criar campanha', this.campaign);
    this.onClose();
  }
}
