import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'imgUrl'
})
export class ImgUrlPipe implements PipeTransform {
  baseApiUrl = 'http://localhost:5000/';

  transform(value: string | null): string | null {
    if(!value) return null;
    return `${this.baseApiUrl}${value}`;
  }

}
