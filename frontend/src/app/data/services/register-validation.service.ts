import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class RegisterValidationService {
  MatchPassword(controlName: string, matchingControlName: string): ValidatorFn {
    return (formGroup: AbstractControl): ValidationErrors | null => {
      const group = formGroup as FormGroup;
      const control = group.controls[controlName];
      const matchingControl = group.controls[matchingControlName];

      if (!control || !matchingControl) {
        return null;
      }

      if (matchingControl.errors && !matchingControl.errors['MatchPassword']) {
        return null;
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ MatchPassword: true });
      } else {
        matchingControl.setErrors(null);
      }

      return null;
    };
  }
}
