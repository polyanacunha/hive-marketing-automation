import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { CardAdComponent } from '../card-ad/card-ad.component';

interface MediaItem {
  id: number;
  title: string;
  description: string;
  duration: string;
  thumbnailUrl: string;
  status: 'aprovado' | 'aprovando' | 'excluir';
  selected: boolean;
}

@Component({
  selector: 'app-gallery',
  standalone: true,
  imports: [CommonModule, FormsModule, SidebarComponent, NavbarComponent, CardAdComponent],
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {
  sidebarOpen = false;
  selectedCategory = 'Todas Categorias';
  selectedItems: MediaItem[] = [];
  
  categories = [
    'Todas Categorias',
    'Vídeos Aprovados',
    'Em Aprovação',
    'Rascunhos'
  ];

  mediaItems: MediaItem[] = [
    {
      id: 1,
      title: 'Salto de luxo - Nike',
      description: 'Descrição sobre o produto',
      duration: '2:30',
      thumbnailUrl: 'assets/video-thumb1.jpg',
      status: 'aprovado',
      selected: false
    },
    {
      id: 2,
      title: 'Salto de luxo - Nike',
      description: 'Descrição sobre o produto',
      duration: '1:45',
      thumbnailUrl: 'assets/video-thumb2.jpg',
      status: 'aprovando',
      selected: false
    },
    {
      id: 3,
      title: 'Salto de luxo - Nike',
      description: 'Descrição sobre o produto',
      duration: '3:01',
      thumbnailUrl: 'assets/video-thumb3.jpg',
      status: 'aprovado',
      selected: false
    },
    {
      id: 4,
      title: 'Salto de luxo - Nike',
      description: 'Descrição sobre o produto',
      duration: '2:15',
      thumbnailUrl: 'assets/video-thumb4.jpg',
      status: 'aprovado',
      selected: false
    },
    {
      id: 5,
      title: 'Salto de luxo - Nike',
      description: 'Descrição sobre o produto',
      duration: '1:30',
      thumbnailUrl: 'assets/video-thumb5.jpg',
      status: 'aprovando',
      selected: false
    },
    {
      id: 6,
      title: 'Salto de luxo - Nike',
      description: 'Descrição sobre o produto',
      duration: '2:45',
      thumbnailUrl: 'assets/video-thumb6.jpg',
      status: 'aprovado',
      selected: false
    },
    {
      id: 7,
      title: 'Salto de luxo - Nike',
      description: 'Descrição sobre o produto',
      duration: '1:20',
      thumbnailUrl: 'assets/video-thumb7.jpg',
      status: 'excluir',
      selected: false
    },
    {
      id: 8,
      title: 'Salto de luxo - Nike',
      description: 'Descrição sobre o produto',
      duration: '3:10',
      thumbnailUrl: 'assets/video-thumb8.jpg',
      status: 'aprovando',
      selected: false
    }
  ];

  constructor() {}

  ngOnInit(): void {}

  toggleSidebar(): void {
    this.sidebarOpen = !this.sidebarOpen;
  }

  closeSidebar(): void {
    this.sidebarOpen = false;
  }

  onCategoryChange(category: string): void {
    this.selectedCategory = category;
  }

  createNewVideo(): void {
    console.log('Criar novo vídeo');
    // Implementar navegação para criação de vídeo
  }

  selectItem(item: MediaItem): void {
    item.selected = !item.selected;
    if (item.selected) {
      this.selectedItems.push(item);
    } else {
      this.selectedItems = this.selectedItems.filter(i => i.id !== item.id);
    }
  }

  selectAll(): void {
    const allSelected = this.mediaItems.every(item => item.selected);
    this.mediaItems.forEach(item => {
      item.selected = !allSelected;
    });
    
    if (allSelected) {
      this.selectedItems = [];
    } else {
      this.selectedItems = [...this.mediaItems];
    }
  }

  onCardAction(action: string, item: MediaItem): void {
    switch (action) {
      case 'edit':
        console.log('Editar item:', item);
        break;
      case 'download':
        console.log('Baixar item:', item);
        break;
      case 'delete':
        console.log('Excluir item:', item);
        this.deleteItem(item);
        break;
    }
  }

  deleteItem(item: MediaItem): void {
    this.mediaItems = this.mediaItems.filter(i => i.id !== item.id);
    this.selectedItems = this.selectedItems.filter(i => i.id !== item.id);
  }

  advance(): void {
    if (this.selectedItems.length === 0) {
      alert('Por favor, selecione pelo menos um vídeo para criar a campanha.');
      return;
    }

    console.log('Criando campanha com itens selecionados:', this.selectedItems);
    
    // Here you can implement the logic to create campaign with selected media
    // For example, you could navigate to another step or call an API
    
    // Example: Navigate to campaign creation with selected items
    // this.router.navigate(['/campaign-creation'], { 
    //   state: { selectedMedia: this.selectedItems } 
    // });
    
    // Or show a success message
    alert(`Campanha criada com sucesso com ${this.selectedItems.length} vídeo(s) selecionado(s)!`);
  }

  get filteredItems(): MediaItem[] {
    if (this.selectedCategory === 'Todas Categorias') {
      return this.mediaItems;
    }
    
    const statusMap: { [key: string]: string } = {
      'Vídeos Aprovados': 'aprovado',
      'Em Aprovação': 'aprovando',
      'Rascunhos': 'excluir'
    };
    
    const status = statusMap[this.selectedCategory];
    return this.mediaItems.filter(item => item.status === status);
  }

  get hasSelectedItems(): boolean {
    return this.selectedItems.length > 0;
  }

  get remainingVideos(): number {
    return Math.max(0, 3 - this.selectedItems.length);
  }
} 