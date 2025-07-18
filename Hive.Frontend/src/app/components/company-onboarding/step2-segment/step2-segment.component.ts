import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OnboardingStateService } from '../../../services/onboarding-state.service';

@Component({
  selector: 'app-step2-segment',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './step2-segment.component.html',
  styleUrl: './step2-segment.component.css'
})
export class Step2SegmentComponent {
  @Output() next = new EventEmitter<void>();
  @Output() prev = new EventEmitter<void>();
  form: FormGroup;
  segments = ['Tecnologia', 'Saúde', 'Educação', 'Moda', 'Outro'];

  constructor(private fb: FormBuilder, private onboarding: OnboardingStateService) {
    this.form = this.fb.group({
      segment: [this.onboarding.onboardingData.segment || 'Tecnologia', Validators.required],
      customSegment: [this.onboarding.onboardingData.customSegment || '']
    });
  }

  onContinue() {
    if (this.form.valid) {
      const seg = this.form.value.segment;
      const custom = seg === 'Outro' ? this.form.value.customSegment : '';
      this.onboarding.setSegment(seg, custom);
      this.next.emit();
    } else {
      this.form.markAllAsTouched();
    }
  }

  onPrev() {
    this.prev.emit();
  }
}
