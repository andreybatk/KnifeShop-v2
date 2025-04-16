import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CreateKnifeDto, KnifeBriefly, KnifeInfo } from '../interfaces/knife.interface';

@Injectable({
  providedIn: 'root'
})

export class KnifeService {
  http = inject(HttpClient);
  baseApiUrl = 'http://localhost:5000/api/knife'

  getKnifesPaginated() {
    return this.http.get<KnifeBriefly[]>(`${this.baseApiUrl}/paginated`)
  }

  createKnife(payload: CreateKnifeDto) {
    const formData = new FormData()
    
    formData.append('title', payload.title)
    formData.append('category', payload.category)
    formData.append('price', payload.price.toString())
    formData.append('description', payload.description)
    formData.append('isOnSale', payload.isOnSale.toString())
    formData.append('image', payload.image)

    if(payload.images)
    {
      payload.images.forEach((image, index) => {
        formData.append(`images[${index}]`, image)
      })
    }
    
    if(payload.knifeInfo)
    {
        Object.keys(payload.knifeInfo).forEach(key => {
          if (payload.knifeInfo[key] !== null && payload.knifeInfo[key] !== undefined)
          {
            formData.append(`knifeInfo[${key}]`, payload.knifeInfo[key])
          }
        })
    }
    
    return this.http.post(`${this.baseApiUrl}`, formData);
  }
}