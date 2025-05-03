import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { KnifesPaginated } from '../interfaces/knife.interface';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  http = inject(HttpClient);
  private baseUrl = environment.apiUrl
  baseApiUrl = `${this.baseUrl}/api/user`

  addFavoriteKnife(id: number) {
    return this.http.post(`${this.baseApiUrl}/favorite_knife/${id}`, null)
  }
  
  removeFavoriteKnife(id:number) {
    return this.http.delete(`${this.baseApiUrl}/favorite_knife/${id}`)
  }

  getFavoriteKnifesPaginated(page: number, pageSize: number) {
    let params = new HttpParams();

    params = params.set('page', page.toString());
    params = params.set('pageSize', pageSize.toString());

    return this.http.get<KnifesPaginated>(`${this.baseApiUrl}/favorite_knifes`, { params })
  }
}
