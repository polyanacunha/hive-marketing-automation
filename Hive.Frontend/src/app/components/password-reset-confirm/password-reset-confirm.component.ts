import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth/auth.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-password-reset-confirm',
  imports: [FormsModule,CommonModule],
  templateUrl: './password-reset-confirm.component.html',
  styleUrls: ['./password-reset-confirm.component.css']
})
export class PasswordResetConfirmComponent {
  userID: string = '';
  code: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  
  // Validação em tempo real
  passwordErrors: string[] = [];
  showPasswordStrength = false;

  constructor(private authService: AuthService) {}
  async resetPassword() {
    // Validar todos os campos
    if (!this.validateForm()) {
      return;
    }
    
    try {
      // Hash da senha antes de enviar
      const hashedPassword = await this.hashPassword(this.newPassword);
      
      this.authService.resetPassword(this.userID, this.code, hashedPassword).subscribe({
        next: response => {
          console.log('Password reset successful', response);
          this.clearSensitiveData();
          alert('Senha redefinida com sucesso!');
        },
        error: error => {
          console.error('Error resetting password', error);
          alert('Erro ao redefinir senha. Verifique os dados e tente novamente.');
        }
      });
    } catch (error) {
      console.error('Error hashing password', error);
      alert('Erro interno. Tente novamente.');
    }
  }

  // Validação completa do formulário
  private validateForm(): boolean {
    let isValid = true;
    
    // Validar código
    // if (!this.code || this.code.trim().length < 4) {
    //   alert('Código de confirmação inválido');
    //   isValid = false;
    // }
    
    // // Validar userID
    // if (!this.userID || this.userID.trim().length === 0) {
    //   alert('ID do usuário é obrigatório');
    //   isValid = false;
    // }
    
    // Validar força da senha
    if (!this.isPasswordStrong(this.newPassword)) {
      alert('A senha não atende aos critérios de segurança');
      isValid = false;
    }
    
    // Validar confirmação de senha
    if (this.newPassword !== this.confirmPassword) {
      alert('As senhas não coincidem');
      isValid = false;
    }
    
    return isValid;
  }

  // Validação de força da senha
  private isPasswordStrong(password: string): boolean {
    this.passwordErrors = [];
    
    if (password.length < 8) {
      this.passwordErrors.push('Mínimo 8 caracteres');
    }
    
    if (!/[A-Z]/.test(password)) {
      this.passwordErrors.push('Pelo menos 1 letra maiúscula');
    }
    
    if (!/[a-z]/.test(password)) {
      this.passwordErrors.push('Pelo menos 1 letra minúscula');
    }
    
    if (!/\d/.test(password)) {
      this.passwordErrors.push('Pelo menos 1 número');
    }
    
    if (!/[!@#$%^&*(),.?":{}|<>]/.test(password)) {
      this.passwordErrors.push('Pelo menos 1 caractere especial');
    }
    
    // Verificar sequências comuns
    if (/123456|abcdef|qwerty|password/i.test(password)) {
      this.passwordErrors.push('Não use sequências comuns');
    }
    
    return this.passwordErrors.length === 0;
  }

  // Verificar força da senha em tempo real
  onPasswordChange(): void {
    this.showPasswordStrength = this.newPassword.length > 0;
    this.isPasswordStrong(this.newPassword);
  }

  // Métodos auxiliares para validação visual
  hasUpperCase(): boolean {
    return /[A-Z]/.test(this.newPassword);
  }

  hasLowerCase(): boolean {
    return /[a-z]/.test(this.newPassword);
  }

  hasNumber(): boolean {
    return /\d/.test(this.newPassword);
  }

  hasSpecialChar(): boolean {
    return /[!@#$%^&*(),.?":{}|<>]/.test(this.newPassword);
  }

  // Verificar se o formulário está válido
  isFormValid(): boolean {
    return this.newPassword === this.confirmPassword &&
           this.isPasswordStrong(this.newPassword);
  }

  // Limpar dados sensíveis
  private clearSensitiveData(): void {
    this.newPassword = '';
    this.confirmPassword = '';
    this.code = '';
    this.userID = '';
    this.passwordErrors = [];
  }

  // Criptografia da senha
  private async hashPassword(password: string): Promise<string> {
    // Gerar um salt aleatório para cada hash
    const salt = this.generateSalt();
    
    // Combinar senha + salt + timestamp para maior segurança
    const timestamp = Date.now().toString();
    const saltedPassword = password + salt + timestamp;
    
    // Usando Web Crypto API nativa do browser
    const encoder = new TextEncoder();
    const data = encoder.encode(saltedPassword);
    const hashBuffer = await crypto.subtle.digest('SHA-256', data);
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    const hashHex = hashArray.map(b => b.toString(16).padStart(2, '0')).join('');
    
    // Retornar hash + salt + timestamp (para o backend poder verificar)
    return `${hashHex}:${salt}:${timestamp}`;
  }

  private generateSalt(): string {
    // Gerar salt aleatório de 16 bytes
    const saltArray = new Uint8Array(16);
    crypto.getRandomValues(saltArray);
    return Array.from(saltArray, byte => byte.toString(16).padStart(2, '0')).join('');
  }
} 