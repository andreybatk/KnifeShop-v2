import { Component, inject } from '@angular/core';
import { Category } from '../../data/interfaces/category.interface';
import { CategoryService } from '../../data/services/category.service';
import { CommonModule } from '@angular/common';
import { ImgUrlPipe } from '../../helpers/pipes/img-url.pipe';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-categories',
  imports: [CommonModule, ImgUrlPipe, RouterModule],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent {
  categories: Category[] = [];
  categoryService = inject(CategoryService);

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (res) => {
        this.categories = res;
      },
      error: () => {
      }
    });
  }
}
