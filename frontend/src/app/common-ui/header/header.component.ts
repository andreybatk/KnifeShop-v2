import { Component, HostListener, ElementRef, inject, OnInit } from '@angular/core';
import { AuthService } from '../../auth/auth.service';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterModule } from '@angular/router';
import { Category } from '../../data/interfaces/category.interface';
import { CategoryService } from '../../data/services/category.service';

@Component({
  selector: 'app-header',
  imports: [CommonModule, RouterModule, RouterLink, RouterLinkActive],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit{
  categoryService = inject(CategoryService)
  authService = inject(AuthService)
  eRef:ElementRef = inject(ElementRef);
  isDropdownOpen = false;
  categories: Category[] = [];

  ngOnInit() {
    this.categoryService.getCategories().subscribe({
      next: (res) => this.categories = res,
      error: (err) => console.error(err)
    });
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: Event) {
    if (!this.eRef.nativeElement.contains(event.target)) {
      this.isDropdownOpen = false;
    }
  }
}
