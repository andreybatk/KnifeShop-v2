import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CreateKnifeDto, Knife, KnifeBriefly, KnifeInfo } from '../interfaces/knife.interface';

@Injectable({
  providedIn: 'root'
})

export class KnifeService {
  http = inject(HttpClient);
  baseApiUrl = 'http://localhost:5000/api/knife'

  getKnifesPaginated() {
    return this.http.get<KnifeBriefly[]>(`${this.baseApiUrl}/paginated`)
  }

  getKnife(id: number) {
    return this.http.get<Knife>(`${this.baseApiUrl}/${id}`)
  }

  createKnife(payload: CreateKnifeDto, image: File | null = null, images: File[]) {
    const formData = new FormData()
    
    formData.append('title', payload.title)
    formData.append('category', payload.category)
    formData.append('price', payload.price.toString())
    formData.append('isOnSale', payload.isOnSale.toString())

    if(payload.description)
    {
      formData.append('description', payload.description)
    }

    if (image) {
      formData.append('image', image, image.name);
    }

    if(images)
    {
      images.forEach((img) => {
        formData.append('images', img, img.name);
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