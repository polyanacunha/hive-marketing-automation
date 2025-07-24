import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  constructor(private http: HttpClient) {}

  createClientProfile(data: {
    marketSegmentId: number;
    targetAudienceId: number;
    companyName: string;
    webSiteUrl: string;
    taxId: string;
  }): Observable<void> {
    const url = `${environment.apiUrl}/api/client/profile`;
    return this.http.post<void>(url, data);
  }
}
