import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { KnifeBriefly } from '../interfaces/knife-briefly.interface';

@Injectable({
  providedIn: 'root'
})
export class KnifeService {
  http = inject(HttpClient);
  baseApiUrl = 'http://localhost:5000/api/knife/'

  getKnifesPaginated() {
    return this.http.get<KnifeBriefly[]>(`${this.baseApiUrl}paginated`)
  }
}