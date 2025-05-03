import { Pipe, PipeTransform } from '@angular/core';
import { environment } from '../../../environments/environment';

@Pipe({
  name: 'imgUrl'
})
export class ImgUrlPipe implements PipeTransform {
  private baseUrl = environment.apiUrl
  baseApiUrl = `${this.baseUrl}`

  transform(value: string | null): string | null {
    if(!value) return null;
    return `${this.baseApiUrl}${value}`;
  }

}
