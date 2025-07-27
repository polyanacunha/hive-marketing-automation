import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

interface MediaItem {
  id: number;
  title: string;
  description: string;
  duration: string;
  thumbnailUrl: string;
  status: 'aprovado' | 'excluir';
  selected: boolean;
}

@Component({
  selector: 'app-card-ad',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './card-ad.component.html',
  styleUrls: ['./card-ad.component.css']
})
export class CardAdComponent {
  @Input() item!: MediaItem;
  @Input() isSelected: boolean = false;
  @Output() select = new EventEmitter<MediaItem>();
  @Output() action = new EventEmitter<{ action: string, item: MediaItem }>();

  showDropdown = false;

  onSelect(): void {
    this.select.emit(this.item);
  }

  onAction(action: string): void {
    this.action.emit({ action, item: this.item });
    this.showDropdown = false;
  }

  toggleDropdown(): void {
    this.showDropdown = !this.showDropdown;
  }

  getStatusColor(): string {
    switch (this.item.status) {
      case 'aprovado':
        return '#4CAF50';
      case 'excluir':
        return '#F44336';
      default:
        return '#9E9E9E';
    }
  }

  getStatusText(): string {
    switch (this.item.status) {
      case 'aprovado':
        return 'Aprovado';
      case 'excluir':
        return 'Excluir';
      default:
        return 'Unknown';
    }
  }
} 