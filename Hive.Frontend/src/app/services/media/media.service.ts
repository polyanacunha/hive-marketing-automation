import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";

interface MIDIA {
  images: File[]; 
}

@Injectable({ providedIn: 'root' })
export class MediaService {
  constructor(private http: HttpClient) {}
  apiUrl = 'https://localhost:7143/api';

  createAds(media: MIDIA): Observable<any> {
    return this.http.post(`${this.apiUrl}/media/create-video`, media);
  }

  uploadImages(media: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/asset/upload`, media);
  }
}