import { Component, Input } from '@angular/core';
import { KnifePaginated } from '../../data/interfaces/knife-paginated.interface';
import { ImgUrlPipe } from '../../helpers/pipes/img-url.pipe';

@Component({
  selector: 'app-knife-card',
  imports: [ImgUrlPipe],
  templateUrl: './knife-card.component.html',
  styleUrl: './knife-card.component.scss'
})
export class KnifeCardComponent {
  @Input() knife!: KnifePaginated;
}
