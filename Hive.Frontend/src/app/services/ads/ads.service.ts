import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs/internal/Observable";
import { HttpClient } from "@angular/common/http";

  @Injectable({ providedIn: 'root' })
export class AdsService {
  constructor(private http: HttpClient) {}

  createAds(ClientObservations: string, InputImagesId: number[]): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/media/create-video`, {ClientObservations, InputImagesId});
  }

  getImagesProductsOfClient(): Observable<any[]> {
    return this.http.get<any[]>(`${environment.apiUrl}/api/media/get-images-products`);
  }
}