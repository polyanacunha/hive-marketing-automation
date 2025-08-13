import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

export interface OnboardingData {
  companyName: string;
  segment: string;
  customSegment: string;
  audience: string;
  customAudience: string;
}

@Injectable({
  providedIn: 'root'
})
export class OnboardingStateService {
  private apiUrl = 'https://localhost:7143/api';

  constructor(private http: HttpClient) {}

  private data: OnboardingData = {
    companyName: '',
    segment: '',
    customSegment: '',
    audience: '',
    customAudience: ''
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
  }

  setAudience(audience: string, customAudience: string = '') {
    this.data.audience = audience;
    this.data.customAudience = customAudience;
  }

  reset() {
    this.data = {
      companyName: '',
      segment: '',
      customSegment: '',
      audience: '',
      customAudience: ''
    };
  }

  getMarketSegments() {
    return this.http.get(`${this.apiUrl}/client/market-segment`);
  }
}
