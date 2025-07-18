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
}
