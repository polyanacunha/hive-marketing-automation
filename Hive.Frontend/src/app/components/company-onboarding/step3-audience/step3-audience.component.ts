import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OnboardingStateService } from '../../../services/onboarding/onboarding-state.service';

@Component({
  selector: 'app-step3-audience',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './step3-audience.component.html',
  styleUrl: './step3-audience.component.css'
})
export class Step3AudienceComponent {
  @Output() next = new EventEmitter<void>();
  @Output() prev = new EventEmitter<void>();
  form: FormGroup;
  audiences = ['Jovens adultos', 'Adultos', 'Mães', 'Terceira idade', 'Outro'];

  constructor(private fb: FormBuilder, private onboarding: OnboardingStateService) {
    this.form = this.fb.group({
      audience: [this.onboarding.onboardingData.audience || 'Jovens adultos', Validators.required],
      customAudience: [this.onboarding.onboardingData.customAudience || '']
    });
  }

  onContinue() {
    if (this.form.valid) {
      const aud = this.form.value.audience;
      const custom = aud === 'Outro' ? this.form.value.customAudience : '';
      this.onboarding.setAudience(aud, custom);
      this.next.emit();
    } else {
      this.form.markAllAsTouched();
    }
  }

  onPrev() {
    this.prev.emit();
  }
}
