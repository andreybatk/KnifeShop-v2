import { Component, inject, OnInit } from '@angular/core';
import { KnifeService } from '../../../data/services/knife.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FileValidationService } from '../../../data/services/file-validation.service';
import { CommonModule } from '@angular/common';
import { Category } from '../../../data/interfaces/category.interface';
import { CategoryService } from '../../../data/services/category.service';
import { NgSelectModule } from '@ng-select/ng-select';

@Component({
  selector: 'app-create-knife-page',
  imports: [ReactiveFormsModule, CommonModule, NgSelectModule],
  templateUrl: './create-knife-page.component.html',
  styleUrl: './create-knife-page.component.scss',
  providers: [FileValidationService]
})
export class CreateKnifePageComponent implements OnInit {
  knifeService = inject(KnifeService)
  router = inject(Router)
  fileValidation = inject(FileValidationService)
  categoryService = inject(CategoryService)
  allCategories: Category[] | null = null

  ngOnInit() {
    this.categoryService.getCategories().subscribe(c => this.allCategories = c)
  }

  form = new FormGroup({
    title: new FormControl(null, Validators.required),
    categoryIds: new FormControl([], Validators.required),
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

  imageFile: File | null = null
  imageFiles: File[] = []

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
