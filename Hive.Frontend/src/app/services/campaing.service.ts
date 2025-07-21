import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CampaingDTO } from '../models/campaing.dto';
import { environment } from '../../environments/environment';

// Export the CreateCampaignParams interface
export interface CreateCampaignParams {
  campaignName: string;
  budget: number;
  initialDate: string;
  endDate: string;
  objectiveCampaignId: number;
  productDescription: string; // Add this field
}

@Injectable({ providedIn: 'root' })
export class CampaignService {
  private url = `${environment.apiUrl}/api/campaing`;
  private url2 = `${environment.apiUrl}/api/campaign/create`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<CampaingDTO[]> {
    return this.http.get<CampaingDTO[]>(this.url);
  }
  getById(id: number): Observable<CampaingDTO> {
    return this.http.get<CampaingDTO>(`${this.url}/${id}`);
  }
  create(params: CreateCampaignParams): Observable<CampaingDTO> {
    // Convert dates to ISO strings
    const initialDateISO = new Date(params.initialDate).toISOString();
    const endDateISO = new Date(params.endDate).toISOString();

    // Create a new object with the converted dates
    const paramsWithISO = {
      ...params,
      initialDate: initialDateISO,
      endDate: endDateISO
    };

    return this.http.post<CampaingDTO>(this.url2, paramsWithISO);
  }
  update(dto: CampaingDTO): Observable<void> {
    return this.http.put<void>(`${this.url}/${dto.id}`, dto);
  }
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
