import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7143/api/auth';

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, { email, password });
  }

  loginWithGoogle(token: string): Observable<any> {
    const url = `${this.apiUrl}/login-google`;
    return this.http.post(url, { token });
  }

  loginWithFacebook(token: string): Observable<any> {
    const url = `${this.apiUrl}/login/facebook`;
    return this.http.post(url, { token });
  }

  resetPassword(userId: string, token: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/reset-password`, { userId, token, password });
  }

  forgotPassword(email: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/forgot-password`, { email });
  }

  confirmEmail(token: string, userId: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/confirm-email`, { token, userId });
  }

}
