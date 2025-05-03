import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Category } from '../interfaces/category.interface';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class CategoryService {
  http = inject(HttpClient);
  private baseUrl = environment.apiUrl
  baseApiUrl = `${this.baseUrl}/api/category`

  getCategories() {
    return this.http.get<Category[]>(`${this.baseApiUrl}`);
  }

  getCategory(id: number) {
    return this.http.get<Category>(`${this.baseApiUrl}/${id}`);
  }

  addCategory(name: string, image: File | null = null) {
    const formData = new FormData()
    formData.append('name', name)

    if (image) {
      formData.append('image', image, image.name);
    }

    return this.http.post(`${this.baseApiUrl}`, formData );
  }
    
  deleteCategory(id: number) {
    return this.http.delete(`${this.baseApiUrl}/${id}`);
  }

  moveCategory(id: number, isMoveUp: boolean) {
    return this.http.patch(`${this.baseApiUrl}/${id}`, { isMoveUp } );
  }
}