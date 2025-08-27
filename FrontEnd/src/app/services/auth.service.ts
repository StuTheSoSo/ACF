import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_KEY = 'jwt_token';
  private readonly CURRENT_USER_KEY = "current_user";

  constructor(private router: Router) { }

  setUser(currentUser: string) {
    localStorage.setItem(this.CURRENT_USER_KEY, currentUser);
  }

  // Check if the user is authenticated by verifying the existence and validity of the JWT
  isAuthenticated(): boolean {
    const token = localStorage.getItem(this.TOKEN_KEY);
    if (!token) {
      return false;
    }
    return this.isTokenValid(token);
  }


  // Logout method to clear the JWT
  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.CURRENT_USER_KEY)
    this.router.navigate(['login']);
  }

  // Get the JWT token
  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  // Optional: Validate JWT (e.g., check if it's expired)
  private isTokenValid(token: string): boolean {
    try {
      // Decode the JWT payload (base64 decoding)
      const payload = JSON.parse(atob(token.split('.')[1]));
      // Check if the token has an expiration (exp) claim and if it's still valid
      if (payload.exp) {
        const expirationDate = new Date(payload.exp * 1000); // Convert to milliseconds
        return expirationDate > new Date();
      }
      return true; // If no expiration, assume valid (adjust based on your needs)
    } catch (error) {
      console.error('Invalid JWT:', error);
      return false;
    }
  }

  getCurrentUser(): string | null {
    return localStorage.getItem(this.CURRENT_USER_KEY);
  }

  getUserRole(): string {
    const token = this.getToken();
    if (token) {
      try {
        const decoded: JwtPayload = jwtDecode(token);
        return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']; // Extract the role claim
      } catch (error) {
        console.error('Error decoding JWT token:', error);
        return '';
      }
    }
    return '';
  }

}

export interface JwtPayload {
  sub: string;
  email: string;
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string; // Support single role or array of roles
  exp: number;
}

