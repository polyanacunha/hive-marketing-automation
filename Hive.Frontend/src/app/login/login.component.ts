import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  loginForm: FormGroup;
  error: string | null = null;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      remember: [false],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.authService.login(email, password).subscribe({
        next: () => {
          this.error = null;
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          this.error = 'E-mail ou senha inválidos';
        },
      });
    }
  }

  loginWithGoogle() {
    this.authService.loginWithGoogle('google-auth-token').subscribe({
      next: (res) => {
        this.error = null;
        console.log('Usuário autenticado com Google:', res);
      },
      error: (err) => {
        this.error = 'Erro ao autenticar com o Google';
      },
    });
  }

  loginWithFacebook() {
    this.authService.loginWithFacebook('facebook-auth-token').subscribe({
      next: (res) => {
        this.error = null;
        console.log('Usuário autenticado com Facebook:', res);
      },
      error: (err) => {
        this.error = 'Erro ao autenticar com o Facebook';
      },
    });
  }
}
