import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  registerForm: FormGroup;
  error: string | null = null;
  success: string | null = null;

  constructor(private fb: FormBuilder, private userService: UserService) {
    this.registerForm = this.fb.group(
      {
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validators: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(form: FormGroup) {
    const password = form.get('password')?.value;
    const confirmPassword = form.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.error = null;
      this.success = null;
      const { firstName, lastName, email, password } = this.registerForm.value;
      const payload = {
        email,
        password,
      };
      this.userService.register(payload).subscribe({
        next: (res) => {
          this.success = res?.message || 'Cadastro realizado com sucesso!';
        },
        error: (err) => {
          this.error =
            err?.error?.Errors?.[0] || 'Erro ao registrar. Tente novamente.';
        },
      });
    } else {
      this.registerForm.markAllAsTouched();
    }
  }
}
