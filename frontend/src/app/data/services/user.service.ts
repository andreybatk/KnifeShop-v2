import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { KnifeBriefly } from '../interfaces/knife.interface';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  http = inject(HttpClient);
  baseApiUrl = 'http://localhost:5000/api/user'

  addFavoriteKnife(id: number) {
    return this.http.post(`${this.baseApiUrl}/favorite_knife`, { id })
  }
  
  removeFavoriteKnife(id:number) {
    return this.http.delete(`${this.baseApiUrl}/favorite_knife/${id}`)
  }

  getFavoriteKnifes() {
    return this.http.get<KnifeBriefly[]>(`${this.baseApiUrl}/favorite_knifes`)
  }
}
