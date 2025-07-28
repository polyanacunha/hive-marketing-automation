import { Component } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-reset-password',
  imports: [FormsModule],
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent {
  email: string = '';

  constructor(private authService: AuthService) {}
  forgotPassword() {
  this.authService.forgotPassword(this.email).subscribe({
      next: response => {
        console.log('Password reset email sent successfully', response);
      },
      error: error => {
        console.error('Error sending password reset email', error);
      }
    });
  }
}