import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { RoleDto } from '../interfaces/role.interface';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  http = inject(HttpClient);
  private baseUrl = environment.apiUrl
  baseApiUrl = `${this.baseUrl}/api/role`

  getUserRoles(email: string | null) {
    const params = email ? new HttpParams().set('email', email) : undefined;
    return this.http.get<string[]>(`${this.baseApiUrl}/user`, { params });
  }

  getRoles() {
    return this.http.get<string[]>(`${this.baseApiUrl}`);
  }

  addRoles(payload: RoleDto) {
    return this.http.post(`${this.baseApiUrl}`, payload);
  }
  
  deleteRoles(payload: RoleDto) {
    return this.http.delete(`${this.baseApiUrl}`, { body: payload });
  }
}
