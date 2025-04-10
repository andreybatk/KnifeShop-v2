import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { KnifePaginated } from '../interfaces/knife-paginated.interface';

@Injectable({
  providedIn: 'root'
})
export class KnifeService {
  http = inject(HttpClient);
  baseApiUrl = 'http://localhost:5000/api/knife/'

  getKnifesPaginated() {
    return this.http.get<KnifePaginated[]>(`${this.baseApiUrl}paginated`)
  }
}