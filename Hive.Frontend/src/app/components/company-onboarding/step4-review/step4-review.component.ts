import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OnboardingStateService } from '../../../services/onboarding/onboarding-state.service';
import { ProfileService } from '../../../services/profile/profile.service';

@Component({
  selector: 'app-step4-review',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './step4-review.component.html',
  styleUrl: './step4-review.component.css'
})
export class Step4ReviewComponent {
  @Output() prev = new EventEmitter<void>();
  onboardingData: any;
  error: string | null = null;
  success: boolean = false;

  constructor(private onboarding: OnboardingStateService, private profileService: ProfileService) {
    this.onboardingData = this.onboarding.onboardingData;
  }

  onPrev() {
    this.prev.emit();
  }

  onFinish() {
    this.error = null;
    this.success = false;
    const profileData = {
      marketSegmentId: this.onboardingData.marketSegmentId || 0,
      targetAudienceId: this.onboardingData.targetAudienceId || 0,
      companyName: this.onboardingData.companyName,
      webSiteUrl: this.onboardingData.webSiteUrl || '',
      taxId: this.onboardingData.taxId || ''
    };
    this.profileService.createClientProfile(profileData).subscribe({
      next: () => {
        this.success = true;
        alert('Cadastro da empresa finalizado!');
      },
      error: (err) => {
        this.error = 'Erro ao criar o perfil. Tente novamente.';
      }
    });
  }
}
