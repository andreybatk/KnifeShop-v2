import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { RegisterValidationService } from '../../data/services/register-validation.service';
import { CommonModule } from '@angular/common';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-register-page',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.scss',
  providers: [RegisterValidationService]
})

export class RegisterPageComponent {
  authService = inject(AuthService)
  registerValidation = inject(RegisterValidationService)
  router = inject(Router)
  isPasswordVisible = signal<boolean>(false)
  errorMessage: string | null = null;

  form = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    username: new FormControl(null, Validators.required),
    password: new FormControl(null, Validators.required),
    confirmPassword: new FormControl(null, Validators.required),
  },
    this.registerValidation.MatchPassword('password', 'confirmPassword')
  ) 
  
  onSubmit() {
    if (this.form.valid) {
      //@ts-ignore
      this.authService.register(this.form.value)
        .pipe(
          catchError(error => {
            if(error?.error?.errors[0]?.description)
            {
              this.errorMessage = error?.error?.errors[0]?.description;
            }
            else
            {
              this.errorMessage = 'Ошибка регистрации.'
            }
            return throwError(() => error);
          })
        )
        .subscribe({
          next: () => {
            this.errorMessage = ''
            this.router.navigate(['login']);
          },
          error: () => {
          }
        });
    }
  }

  onLoginSubmit() {
    this.router.navigate(['login'])
  }
}
