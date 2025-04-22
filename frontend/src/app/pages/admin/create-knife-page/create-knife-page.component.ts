import { Component, inject } from '@angular/core';
import { KnifeService } from '../../../data/services/knife.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FileValidationService } from '../../../data/services/file-validation.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-create-knife-page',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './create-knife-page.component.html',
  styleUrl: './create-knife-page.component.scss',
  providers: [FileValidationService]
})
export class CreateKnifePageComponent {
  knifeService = inject(KnifeService)
  router = inject(Router)
  fileValidation = inject(FileValidationService)

  form = new FormGroup({
    title: new FormControl(null, Validators.required),
    category: new FormControl(null, Validators.required),
    price: new FormControl(null, [Validators.required, Validators.min(0)]),
    description: new FormControl(null),
    isOnSale: new FormControl(true),
    image: new FormControl(null, this.fileValidation.FileTypeValidator()),
    images: new FormControl(null, this.fileValidation.FileTypeValidator()),
    knifeInfo: new FormGroup({
      overallLength: new FormControl(null),
      bladeLength: new FormControl(null),
      buttThickness: new FormControl(null),
      weight: new FormControl(null),
      handleMaterial: new FormControl(null),
      country: new FormControl(null),
      manufacturer: new FormControl(null),
      steelGrade: new FormControl(null),
    })
  });

  imageFile: File | null = null;
  imageFiles: File[] = [];

  onImageSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.imageFile = input.files[0];
    }
  }

  onImagesSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.imageFiles = Array.from(input.files);
    }
  }

  onSubmit() {
    if (this.form.valid) {
      //@ts-ignore
      this.knifeService.createKnife(this.form.value, this.imageFile, this.imageFiles).subscribe(id => {
        this.router.navigate([`knife/${id}`])
      })
    }
  }
}
