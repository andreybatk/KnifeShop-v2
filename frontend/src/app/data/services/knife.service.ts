import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CreateKnifeDto, EditKnifeDto, GetKnifesPaginationDto, Knife, KnifeBriefly, KnifesPaginated } from '../interfaces/knife.interface';

@Injectable({
  providedIn: 'root'
})

export class KnifeService {
  http = inject(HttpClient);
  baseApiUrl = 'http://localhost:5000/api/knife'

  getKnifesPaginated(request: GetKnifesPaginationDto) {
    let params = new HttpParams();
  
    if (request.search) {
      params = params.set('search', request.search);
    }
    if (request.sortItem) {
      params = params.set('sortItem', request.sortItem);
    }
    if (request.sortOrder) {
      params = params.set('sortOrder', request.sortOrder);
    }
    if (request.page !== null) {
      params = params.set('page', request.page.toString());
    }
    if (request.pageSize !== null) {
      params = params.set('pageSize', request.pageSize.toString());
    }
  
    return this.http.get<KnifesPaginated>(`${this.baseApiUrl}/paginated`, { params });
  }

  getKnifesOnSale() {
    return this.http.get<KnifeBriefly[]>(`${this.baseApiUrl}/on_sale`)
  }

  getKnife(id: number) {
    return this.http.get<Knife>(`${this.baseApiUrl}/${id}`)
  }

  createKnife(payload: CreateKnifeDto, image: File | null = null, images: File[]) {
    const formData = this.knifeAction(payload, image, images)
    return this.http.post(`${this.baseApiUrl}`, formData);
  }

  deleteKnife(id:number) {
    return this.http.delete(`${this.baseApiUrl}/${id}`);
  }

  editKnife(payload: EditKnifeDto, image: File | null = null, images: File[]) {
    const formData = this.knifeAction(payload, image, images)
    return this.http.put(`${this.baseApiUrl}/${payload.id}`, formData);
  }

  private knifeAction(payload: CreateKnifeDto | EditKnifeDto, image: File | null = null, images: File[]) : FormData {
    const formData = new FormData()
    
    formData.append('title', payload.title)
    
    payload.categoryIds.forEach(id => {
        formData.append('categoryIds', id.toString());
    })
   
    formData.append('price', payload.price.toString())
    formData.append('isOnSale', payload.isOnSale.toString())

    if(payload.description) {
      formData.append('description', payload.description)
    }

    if (image) {
      formData.append('image', image, image.name);
    }

    if(images) {
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

    return formData;
  }
}