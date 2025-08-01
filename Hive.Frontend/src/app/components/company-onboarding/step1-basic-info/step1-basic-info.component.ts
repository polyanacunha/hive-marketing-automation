import { Component, EventEmitter, Output } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OnboardingStateService } from '../../../services/onboarding/onboarding-state.service';

@Component({
  selector: 'app-step1-basic-info',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './step1-basic-info.component.html',
  styleUrl: './step1-basic-info.component.css',
})
export class Step1BasicInfoComponent {
  @Output() next = new EventEmitter<void>();
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private onboarding: OnboardingStateService
  ) {
    this.form = this.fb.group({
      companyName: [
        this.onboarding.onboardingData.companyName,
        [Validators.required, Validators.minLength(2)],
      ],
    });
  }

  onContinue() {
    if (this.form.valid) {
      this.onboarding.setCompanyName(this.form.value.companyName);
      this.next.emit();
    } else {
      this.form.markAllAsTouched();
    }
  }
}
