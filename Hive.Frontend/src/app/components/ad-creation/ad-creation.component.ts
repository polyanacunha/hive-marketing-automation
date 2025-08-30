import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MediaService } from '../../services/media/media.service';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AdsService } from '../../services/ads/ads.service';
import { Router } from '@angular/router';

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
  styleUrl: './ad-creation.component.css',
})
export class AdCreationComponent implements OnInit {
  @Output() close = new EventEmitter<void>();
  @Output() imagesSelected = new EventEmitter<File[]>();
  constructor(private mediaService: MediaService, private adsService: AdsService, private router: Router) {}
  selectedImages: SelectedImage[] = [];
  isVisible = false;
  images: { url: string; name: string }[] = [];

  ngOnInit(): void {
    this.getImageIdsFromBucket().subscribe((response: any[]) => {
      this.images = response.map((img: any) => ({
        url: img.url,
        name: this.extractFileName(img.url)
      }));
    });
  }

  closeModal(): void {
    this.close.emit();
  }
  emitSelectedImages(): void {
    const files = this.selectedImages.map((img) => img.file);
    this.imagesSelected.emit(files);
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files) return;
    Array.from(input.files).forEach((file) => {
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
      Array.from(event.dataTransfer.files).forEach((file) => {
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

  uploadImages(): void {
    const formData = new FormData();

    formData.append('AlbumName', 'YourAlbumName');

    this.selectedImages.forEach((img) => {
      formData.append('Files', img.file);
    });

    this.mediaService.uploadImages(formData).subscribe({
      next: (response) => {
        console.log('Images uploaded successfully', response);
      },
      error: (error) => {
        console.error('Error uploading images', error);
      },
    });
  }

  getImageIdsFromBucket(): Observable<any[]> {
    return this.mediaService.getImageIdsFromBucket().pipe(
      tap((response: any[]) => {
        console.log('Images fetched successfully', response);
        response.map((img: any) => ({
          url: img.url,
        }));
      })
    );
  }

  createAds(){
    //salva no bucket
    this.uploadImages();
    //busca os ids do bucket
    this.getImageIdsFromBucket().subscribe((imageIds: number[]) => {
      const clientObservations = 'Your observations here';
      const inputImagesId = imageIds;

    //cria o anúncio
      this.adsService.createAds(clientObservations, inputImagesId).subscribe({
        next: (response) => {
          console.log('Ads created successfully', response);
        },
        error: (error) => {
          console.error('Error creating ads', error);
        },
      });

    });
      this.router.navigate(['/media-gallery']);
  }

  private extractFileName(url: string): string {
    try {
      const lastSlash = url.lastIndexOf('/');
      return lastSlash >= 0 ? url.substring(lastSlash + 1) : url;
    } catch {
      return 'image';
    }
  }

  
}
