import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { RegisterValidationService } from '../../data/services/register-validation.service';
import { CommonModule } from '@angular/common';

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
  errors: string[] = [];

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
        .subscribe({
          next: () => {
            this.router.navigate(['login']);
          },
          error: (err) => {
            if (err?.error) {
              this.errors = err.error.map((e: any) => e.description);
            } else {
              this.errors = ['Произошла непредвиденная ошибка. Попробуйте снова.'];
            }
          }
        });
    }
  }

  onLoginSubmit() {
    this.router.navigate(['login'])
  }
}
