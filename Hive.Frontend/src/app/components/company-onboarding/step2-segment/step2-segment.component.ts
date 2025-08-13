import { Component, EventEmitter, Output, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OnboardingStateService } from '../../../services/onboarding/onboarding-state.service';

@Component({
  selector: 'app-step2-segment',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './step2-segment.component.html',
  styleUrl: './step2-segment.component.css'
})
export class Step2SegmentComponent implements OnInit {
  @Output() next = new EventEmitter<void>();
  @Output() prev = new EventEmitter<void>();
  form: FormGroup;
  segments: string[] = [];

  constructor(
    private fb: FormBuilder,
    private onboarding: OnboardingStateService,
  ) {
    this.form = this.fb.group({
      segment: [this.onboarding.onboardingData.segment || '', Validators.required],
      customSegment: [this.onboarding.onboardingData.customSegment || ''],
    });
  }
  
  ngOnInit(): void {
    this.onboarding.getMarketSegments().subscribe({
      next: (response: any) => {
        const items = Array.isArray(response) ? response : (response?.data ?? []);
        this.segments = (items || [])
          .map((item: any) => item?.description ?? item?.Description)
          .filter((s: any) => !!s);
        // If there's no current value, select the first available segment
        const current = this.form.get('segment')?.value;
        if (!current && this.segments.length > 0) {
          this.form.get('segment')?.setValue(this.segments[0]);
        }
      },
      error: (error) => {
        console.error('Error loading market segments', error);
        this.segments = [];
      }
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

  getMarketSegments(){
      this.onboarding.getMarketSegments().subscribe({
        next: response => {
          console.log('Market segments loaded', response);
        },
        error: error => {
          console.error('Error loading market segments', error);
        }
      });
  }
}
