import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryService } from '../../../data/services/category.service';
import { Category } from '../../../data/interfaces/category.interface';
import { CommonModule } from '@angular/common';
import { FileValidationService } from '../../../data/services/file-validation.service';
import { ImgUrlPipe } from '../../../helpers/pipes/img-url.pipe';

@Component({
  selector: 'app-edit-categories-page',
  imports: [ReactiveFormsModule, CommonModule, ImgUrlPipe],
  templateUrl: './edit-category-page.component.html',
  styleUrl: './edit-category-page.component.scss'
})
export class EditCategoryPageComponent implements OnInit{
  categoryService = inject(CategoryService)
  fileValidation = inject(FileValidationService)
  categories: Category[] = []
  errorMessage: string | null = null
  imageFile: File | null = null

  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    image: new FormControl(null, this.fileValidation.FileTypeValidator())
  });

  ngOnInit() {
    this.loadCategories()
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: () => {
        this.errorMessage = 'Не удалось загрузить категории.';
      }
    });
  }

  onSubmit() {
    if (this.form.valid) {
      const name = this.form.value.name;
      if (!name) return;

      this.categoryService.addCategory(name, this.imageFile).subscribe({
        next: () => {
          this.form.reset();
          this.loadCategories();
        },
        error: () => {
          this.errorMessage = 'Не удалось добавить категорию.';
        }
      });
    }
  }

  onDeleteCategory(id: number) {
    if (confirm('Вы уверены, что хотите удалить эту категорию?')) {
      this.categoryService.deleteCategory(id).subscribe({
        next: () => {
          this.loadCategories();
        },
        error: () => {
          this.errorMessage = 'Не удалось удалить категорию.';
        }
      });
    }
  }

  onImageSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.imageFile = input.files[0];
    }
  }
}
