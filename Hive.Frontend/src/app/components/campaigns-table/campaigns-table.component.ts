import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

interface Campaign {
  id: number;
  name: string;
  platform: string;
  status: string;
  budget: number;
  spent: number;
  startDate: Date;
  endDate: Date;
  impressions: number;
  clicks: number;
  ctr: number;
}

@Component({
  selector: 'app-campaigns-table',
  imports: [CommonModule],
  templateUrl: './campaigns-table.component.html',
  styleUrls: ['./campaigns-table.component.css']
})
export class CampaignsTableComponent implements OnInit {
  campaigns: Campaign[] = [];

  ngOnInit() {
    this.loadMockData();
  }

  loadMockData() {
    this.campaigns = [
      {
        id: 1,
        name: 'Campanha Black Friday 2024',
        platform: 'Facebook',
        status: 'Ativa',
        budget: 5000,
        spent: 3200,
        startDate: new Date('2024-11-20'),
        endDate: new Date('2024-11-30'),
        impressions: 125000,
        clicks: 8500,
        ctr: 6.8
      },
      {
        id: 2,
        name: 'Promoção Verão Instagram',
        platform: 'Instagram',
        status: 'Ativa',
        budget: 3000,
        spent: 1800,
        startDate: new Date('2024-12-01'),
        endDate: new Date('2024-12-31'),
        impressions: 89000,
        clicks: 6200,
        ctr: 7.0
      },
      {
        id: 3,
        name: 'Google Ads - Produtos Premium',
        platform: 'Google',
        status: 'Pausada',
        budget: 8000,
        spent: 4500,
        startDate: new Date('2024-10-15'),
        endDate: new Date('2024-11-15'),
        impressions: 200000,
        clicks: 12000,
        ctr: 6.0
      },
      {
        id: 4,
        name: 'TikTok - Influencer Marketing',
        platform: 'TikTok',
        status: 'Finalizada',
        budget: 2500,
        spent: 2500,
        startDate: new Date('2024-09-01'),
        endDate: new Date('2024-09-30'),
        impressions: 150000,
        clicks: 9500,
        ctr: 6.3
      },
      {
        id: 5,
        name: 'Facebook - Conversão Local',
        platform: 'Facebook',
        status: 'Ativa',
        budget: 4000,
        spent: 2100,
        startDate: new Date('2024-12-01'),
        endDate: new Date('2024-12-31'),
        impressions: 95000,
        clicks: 7800,
        ctr: 8.2
      }
    ];
  }

  getPlatformIcon(platform: string): string {
    const icons: { [key: string]: string } = {
      'Facebook': 'fab fa-facebook-f',
      'Instagram': 'fab fa-instagram',
      'Google': 'fab fa-google',
      'TikTok': 'fab fa-tiktok'
    };
    return icons[platform] || 'fas fa-globe';
  }

  editCampaign(campaign: Campaign) {
    console.log('Editar campanha:', campaign);
    // Implementar lógica de edição
  }

  viewCampaign(campaign: Campaign) {
    console.log('Visualizar campanha:', campaign);
    // Implementar lógica de visualização
  }

  deleteCampaign(campaign: Campaign) {
    console.log('Deletar campanha:', campaign);
    // Implementar lógica de exclusão
    if (confirm(`Tem certeza que deseja excluir a campanha "${campaign.name}"?`)) {
      this.campaigns = this.campaigns.filter(c => c.id !== campaign.id);
    }
  }
}
