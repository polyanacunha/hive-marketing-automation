import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MediaService } from '../../services/media/media.service';

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
  constructor(private mediaService: MediaService) {}
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

  uploadAndCreateAds(): void {
    this.uploadImages();
    this.getImagesToCreateAds();
    this.createAds();
  }

  uploadImages(): void {
    const formData = new FormData();
  
    formData.append('AlbumName', 'YourAlbumName');
  
    this.selectedImages.forEach(img => {
      formData.append('Files', img.file);
    });
  
    this.mediaService.uploadImages(formData).subscribe({
      next: (response) => {
        console.log('Images uploaded successfully', response);
      },
      error: (error) => {
        console.error('Error uploading images', error);
      }
    });
  }

  getImagesToCreateAds() {
    this.mediaService.getImagesToCreateAds().subscribe({
      next: (response: any[]) => {
        console.log('Images fetched successfully', response);
        response.map((img: any) => ({
          url: img.url,
        }));
      },
      error: (error) => {
        console.error('Error fetching images', error);
      }
    });
  }
  
  createAds(): void {
    const campaignData = {
      images: this.selectedImages.map(img => img.file),
    };

    this.mediaService.createAds(campaignData).subscribe({
      next: (response) => {
        console.log('Ads created successfully', response);
      },
      error: (error) => {
        console.error('Error creating ads', error);
      }
    });
  }
}
