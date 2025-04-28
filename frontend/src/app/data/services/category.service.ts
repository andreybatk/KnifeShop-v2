import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CreateKnifeDto, EditKnifeDto, GetKnifesPaginationDto, Knife, KnifeBriefly, KnifesPaginated } from '../interfaces/knife.interface';
import { Category } from '../interfaces/category.interface';

@Injectable({
  providedIn: 'root'
})

export class CategoryService {
  http = inject(HttpClient);
  baseApiUrl = 'http://localhost:5000/api/category'

  getCategories() {
    return this.http.get<Category[]>(`${this.baseApiUrl}`);
  }

  addCategory(name: string) {
      return this.http.post(`${this.baseApiUrl}`, { name } );
  }
    
  deleteCategory(id: number) {
    return this.http.delete(`${this.baseApiUrl}/${id}`);
  }
}