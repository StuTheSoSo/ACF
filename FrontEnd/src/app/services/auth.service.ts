import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_KEY = 'jwt_token';

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
}