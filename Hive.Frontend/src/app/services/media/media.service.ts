import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class MediaService {
  constructor(private http: HttpClient) {}
  apiUrl = 'https://localhost:7143/api';

  getImagesToCreateAds(): Observable<any> {
    return this.http.get(`${this.apiUrl}/asset/images`);
  }
  uploadImages(media: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/asset/upload`, media);
  }
}