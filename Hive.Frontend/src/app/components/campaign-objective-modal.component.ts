import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-campaign-objective-modal',
  imports: [CommonModule, FormsModule],
  templateUrl: './campaign-objective-modal.component.html',
  styleUrls: ['./campaign-objective-modal.component.css']
})
export class CampaignObjectiveModalComponent {
  @Output() closeModal = new EventEmitter<void>();

  options = [
    {
      value: 'reconhecimento',
      label: '<b>Reconhecimento:</b> Mostre os anúncios para quem tem maior probabilidade de se lembrar deles.'
    },
    {
      value: 'trafego',
      label: '<b>Tráfego:</b> Direcione as pessoas para um destino, como um site, app, perfil do Instagram ou evento do Facebook'
    },
    {
      value: 'engajamento',
      label: '<b>Engajamento:</b> Aumente o número de mensagens, compras por mensagens, visualizações de vídeos, engajamentos com posts, curtidas na Página ou participações em eventos.'
    },
    {
      value: 'leads',
      label: '<b>Leads:</b> Consiga leads para sua empresa ou marca.'
    },
    {
      value: 'promocao-app',
      label: '<b>Promoção do app:</b> Encontre novas pessoas para instalar seu app e continuar usando-o.'
    },
    {
      value: 'vendas',
      label: '<b>Vendas:</b> Promoção do app: Encontre novas pessoas para instalar seu app e continuar usando-o.'
    }
  ];

  selectedObjective: string | null = null;

  constructor(private router: Router) {}

  close() {
    this.closeModal.emit();
  }

  openCreateCampaignModal() {
    this.router.navigate(['/create-campaign']);
  }
}