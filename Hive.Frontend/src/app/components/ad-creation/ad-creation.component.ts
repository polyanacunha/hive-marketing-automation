import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

interface SelectedImage {
  file: File;
  url: string;
  loading: boolean;
}

@Component({
  selector: 'app-ad-creation',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ad-creation.component.html',
  styleUrl: './ad-creation.component.css'
})
export class AdCreationComponent {
  selectedImages: SelectedImage[] = [];

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files) return;
    Array.from(input.files).forEach(file => {
      if (this.isValidImage(file)) {
        const imgObj: SelectedImage = { file, url: '', loading: true };
        this.selectedImages.push(imgObj);
        const reader = new FileReader();
        reader.onload = (e: any) => {
          imgObj.url = e.target.result;
          imgObj.loading = false;
        };
        reader.readAsDataURL(file);
      }
    });
    input.value = '';
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    if (event.dataTransfer && event.dataTransfer.files) {
      Array.from(event.dataTransfer.files).forEach(file => {
        if (this.isValidImage(file)) {
          const imgObj: SelectedImage = { file, url: '', loading: true };
          this.selectedImages.push(imgObj);
          const reader = new FileReader();
          reader.onload = (e: any) => {
            imgObj.url = e.target.result;
            imgObj.loading = false;
          };
          reader.readAsDataURL(file);
        }
      });
    }
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault();
  }

  removeImage(index: number): void {
    this.selectedImages.splice(index, 1);
  }

  isValidImage(file: File): boolean {
    const validTypes = ['image/jpeg', 'image/png'];
    const maxSize = 2 * 1024 * 1024; // 2MB
    return validTypes.includes(file.type) && file.size <= maxSize;
  }

  onCancel(): void {
    this.selectedImages = [];
  }

  onSave(): void {
    alert('Imagens salvas! (implemente a integração com backend)');
  }
}
