import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-confirm-email',
  imports: [CommonModule, FormsModule],
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {
  token: string = '';
  userId: string = '';
  isLoading: boolean = false;
  isConfirmed: boolean = false;
  errorMessage: string = '';
  isResending: boolean = false;
  userEmail: string = '';
  showEmailInput: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit() {
    // Extrair token e userId dos parâmetros da URL
    this.route.queryParams.subscribe(params => {
      this.token = params['token'] || '';
      this.userId = params['userId'] || '';
      
      if (!this.token || !this.userId) {
        this.errorMessage = 'Token ou ID do usuário inválido.';
      } else {
        // Confirmar email automaticamente
        this.confirmEmailAutomatically();
      }
    });
  }

  confirmEmailAutomatically() {
    this.isLoading = true;
    this.errorMessage = '';

    this.authService.confirmEmail(this.token, this.userId).subscribe({
      next: (response: any) => {
        this.isLoading = false;
        this.isConfirmed = true;
        console.log('Email confirmado com sucesso:', response);
        
        // Redirecionar para company-onboarding após 3 segundos
        // setTimeout(() => {
          this.router.navigate(['/company-onboarding']);
        // }, 3000);
      },
      error: (error: any) => {
        this.isLoading = false;
        this.errorMessage = 'Não foi possível confirmar seu email. Verifique se o link é válido ou tente reenviar o email de confirmação.';
        console.error('Erro na confirmação:', error);
      }
    });
  }

  showResendEmailInput() {
    this.showEmailInput = true;
  }

  resendConfirmationEmail() {
    if (!this.userEmail.trim()) {
      alert('Por favor, digite seu email.');
      return;
    }

    this.isResending = true;

    this.authService.resendConfirmationEmail(this.userEmail).subscribe({
      next: (response: any) => {
        this.isResending = false;
        this.showEmailInput = false;
        alert('Email de redefinição de senha enviado com sucesso! Verifique sua caixa de entrada.');
      },
      error: (error: any) => {
        this.isResending = false;
        console.error('Erro ao reenviar email:', error);
        alert('Erro ao reenviar email. Tente novamente mais tarde.');
      }
    });
  }
}
