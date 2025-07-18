import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Step1BasicInfoComponent } from './step1-basic-info/step1-basic-info.component';
import { Step2SegmentComponent } from './step2-segment/step2-segment.component';
import { Step3AudienceComponent } from './step3-audience/step3-audience.component';
import { Step4ReviewComponent } from './step4-review/step4-review.component';

@Component({
  selector: 'app-company-onboarding',
  standalone: true,
  imports: [CommonModule, Step1BasicInfoComponent, Step2SegmentComponent, Step3AudienceComponent, Step4ReviewComponent],
  templateUrl: './company-onboarding.component.html',
  styleUrl: './company-onboarding.component.css'
})
export class CompanyOnboardingComponent {
  step = 1;
  readonly totalSteps = 4;

  nextStep() {
    if (this.step < this.totalSteps) this.step++;
  }

  prevStep() {
    if (this.step > 1) this.step--;
  }

  goToStep(step: number) {
    if (step >= 1 && step <= this.totalSteps) this.step = step;
  }
}
