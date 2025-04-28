import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryService } from '../../../data/services/category.service';
import { Category } from '../../../data/interfaces/category.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-categories-page',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './edit-category-page.component.html',
  styleUrl: './edit-category-page.component.scss'
})
export class EditCategoryPageComponent implements OnInit{
  categoryService = inject(CategoryService)
  categories: Category[] = []
  errorMessage: string | null = null

  form = new FormGroup({
    name: new FormControl('', [Validators.required]),
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

      this.categoryService.addCategory(name).subscribe({
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
}
