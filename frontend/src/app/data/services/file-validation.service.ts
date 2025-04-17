import { Injectable } from '@angular/core';
import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

export class FileValidationService {
  FileTypeValidator (): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;
      if (!value) return null; 
      const fileName = typeof value === 'string' ? value : value.name;
      const regex = /\.(jpg|png|jpeg)$/i;
      return !regex.test(fileName) ? { notSupportedFileType: true } : null;
    };
  }
}