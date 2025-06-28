import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OnboardingStateService } from '../../../services/onboarding-state.service';

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

  constructor(private onboarding: OnboardingStateService) {
    this.onboardingData = this.onboarding.onboardingData;
  }

  onPrev() {
    this.prev.emit();
  }

  onFinish() {
    // Aqui você pode chamar a API para salvar os dados
    alert('Cadastro da empresa finalizado!');
  }
}
