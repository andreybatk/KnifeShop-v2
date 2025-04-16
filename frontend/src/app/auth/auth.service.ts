import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { JwtPayload, TokenResponse } from './auth.interface';
import { tap, throwError, catchError } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  http = inject(HttpClient)
  cookieService = inject(CookieService)
  router = inject(Router)
  baseApiUrl = 'http://localhost:5000/api/auth/'

  token: string | null = null;
  refreshToken: string | null = null;

  get isAuth() {
    if(!this.token)
    {
      this.token = this.cookieService.get('token')
      this.refreshToken = this.cookieService.get('refreshToken')
    }

    return !!this.token
  }
  
  get username(): string | null {
    return this.cookieService.get('username') || null;
  }

  login(payload: {username: string, password: string}) {
    return this.http.post<TokenResponse>(
      `${this.baseApiUrl}login`,
      payload,
    ).pipe(
      tap(val => this.saveTokens(val, payload.username))
    )
  }

  register(payload: {email: string, username: string, password: string, confirmPassword: string}) {
    return this.http.post(
      `${this.baseApiUrl}register`,
      payload).pipe(
        catchError(error => {
          console.log('Full error object from auth service', error)
          return throwError(() => new Error(error));
        })
      )}

  refreshAuthToken() {
    return this.http.post<TokenResponse>(
      `${this.baseApiUrl}refresh`,
      {
        refreshToken: this.refreshToken
      })
      .pipe(
          tap(val => this.saveTokens(val)),
          catchError(error => {
            this.logout()
            return throwError(() => new Error(error))
          })
      )
  }

  logout() {
    this.cookieService.deleteAll()
    this.token = null
    this.refreshToken = null;
    this.router.navigate(['/login'])
  }

  saveTokens(res: TokenResponse, username: string | null = null) {
    this.token = res.accessToken
    this.refreshToken = res.refreshToken
    
    this.cookieService.set('token', this.token)
    this.cookieService.set('refreshToken', this.refreshToken)

    if(username)
    {
      this.cookieService.set('username', username);
    }
  }

  get userRoles(): string[] {
    if (!this.token) {
      this.token = this.cookieService.get('token');
    }
  
    if (!this.token) return [];
  
    try {
      const decoded = jwtDecode<JwtPayload>(this.token);
      const roles = decoded.role;
      if (!roles) return [];
  
      return Array.isArray(roles) ? roles : [roles];
    } catch (e) {
      return [];
    }
  }
  
  hasRole(role: string): boolean {
    return this.userRoles.includes(role);
  }
}
