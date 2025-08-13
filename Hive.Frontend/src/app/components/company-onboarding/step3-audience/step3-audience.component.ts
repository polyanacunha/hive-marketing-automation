import { Component, EventEmitter, Output, OnInit } from '@angular/core';
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
export class Step3AudienceComponent implements OnInit {
  @Output() next = new EventEmitter<void>();
  @Output() prev = new EventEmitter<void>();
  form: FormGroup;
  audiences: string[] = [];

  constructor(private fb: FormBuilder, private onboarding: OnboardingStateService) {
    this.form = this.fb.group({
      audience: [this.onboarding.onboardingData.audience || '', Validators.required],
      customAudience: [this.onboarding.onboardingData.customAudience || '']
    });
  }

  ngOnInit(): void {
    this.onboarding.getAudiences().subscribe({
      next: (response: any) => {
        const items = Array.isArray(response) ? response : (response?.data ?? []);
        this.audiences = (items || [])
          .map((item: any) => item?.description ?? item?.Description)
          .filter((s: any) => !!s);
        const current = this.form.get('audience')?.value;
        if (!current && this.audiences.length > 0) {
          this.form.get('audience')?.setValue(this.audiences[0]);
        }
      },
      error: (error) => {
        console.error('Error loading audiences', error);
        this.audiences = [];
      }
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

  getAudiences(){
    this.onboarding.getAudiences().subscribe({
      next: (response: any) => {
        console.log('Audiences loaded', response);
      },
      error: (error) => {
        console.error('Error loading audiences', error);
      }
    });
  }
}
