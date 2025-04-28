import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login-page',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss',
  providers: []
})

export class LoginPageComponent {
  authService = inject(AuthService)
  router = inject(Router)
  errorMessage: string = ''

  isPasswordVisible = signal<boolean>(false)

  form = new FormGroup({
    username: new FormControl(null, Validators.required),
    password: new FormControl(null, Validators.required)
  })

  onSubmit() {
    if(this.form.valid) {
      //@ts-ignore
      this.authService.login(this.form.value)
      .pipe(
        catchError(error => {
          this.errorMessage = 'Ошибка авторизации. Неправильный логин или пароль.';
          return throwError(() => error);
        })
      )
      .subscribe({
        next: () => {
          this.router.navigate(['']);
        },
        error: () => {
        }
      });
    }
  }

  onRegisterSubmit() {
    this.router.navigate(['register'])
  }
}
