import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs';

export interface OnboardingData {
  companyName: string;
  segment: string;
  customSegment: string;
  audience: string;
  customAudience: string;
  marketSegmentId: number | null;
  targetAudienceId: number | null;
}

@Injectable({
  providedIn: 'root'
})
export class OnboardingStateService {
  private apiUrl = 'https://localhost:7143/api';
  private marketSegmentsCache: { id: number; description: string }[] = [];
  private audiencesCache: { id: number; description: string }[] = [];

  constructor(private http: HttpClient) {}

  private data: OnboardingData = {
    companyName: '',
    segment: '',
    customSegment: '',
    audience: '',
    customAudience: '',
    marketSegmentId: null,
    targetAudienceId: null
  };

  get onboardingData(): OnboardingData {
    return this.data;
  }

  setCompanyName(name: string) {
    this.data.companyName = name;
  }

  setSegment(segment: string, customSegment: string = '') {
    this.data.segment = segment;
    this.data.customSegment = customSegment;
    const found = this.marketSegmentsCache.find(s => s.description === segment);
    this.data.marketSegmentId = found ? found.id : null;
  }

  setAudience(audience: string, customAudience: string = '') {
    this.data.audience = audience;
    this.data.customAudience = customAudience;
    const found = this.audiencesCache.find(a => a.description === audience);
    this.data.targetAudienceId = found ? found.id : null;
  }

getAudiences(){
  return this.http.get(`${this.apiUrl}/client/target-audience`).pipe(
    tap((response: any) => {
      const items = Array.isArray(response) ? response : (response?.data ?? []);
      const normalized: { id: number; description: string }[] = (items || [])
        .map((item: any) => ({
          id: Number(item?.id ?? item?.Id ?? 0),
          description: String(item?.description ?? item?.Description ?? '')
        }))
        .filter((i: { id: number; description: string }) => !!i.description && !!i.id);
      this.audiencesCache = normalized;
    })
  );
}

  reset() {
    this.data = {
      companyName: '',
      segment: '',
      customSegment: '',
      audience: '',
      customAudience: '',
      marketSegmentId: null,
      targetAudienceId: null
    };
  }

  getMarketSegments() {
    return this.http.get(`${this.apiUrl}/client/market-segment`).pipe(
      tap((response: any) => {
        const items = Array.isArray(response) ? response : (response?.data ?? []);
        const normalized: { id: number; description: string }[] = (items || [])
          .map((item: any) => ({
            id: Number(item?.id ?? item?.Id ?? 0),
            description: String(item?.description ?? item?.Description ?? '')
          }))
          .filter((i: { id: number; description: string }) => !!i.description && !!i.id);
        this.marketSegmentsCache = normalized;
      })
    );
  }
}
