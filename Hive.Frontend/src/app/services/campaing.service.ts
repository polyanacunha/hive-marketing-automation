import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CampaingDTO } from '../models/campaing.dto';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private url = `${environment.apiUrl}/api/campaing`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<CampaingDTO[]> {
    return this.http.get<CampaingDTO[]>(this.url);
  }
  getById(id: number): Observable<CampaingDTO> {
    return this.http.get<CampaingDTO>(`${this.url}/${id}`);
  }
  create(dto: CampaingDTO): Observable<CampaingDTO> {
    return this.http.post<CampaingDTO>(this.url, dto);
  }
  update(dto: CampaingDTO): Observable<void> {
    return this.http.put<void>(`${this.url}/${dto.id}`, dto);
  }
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
