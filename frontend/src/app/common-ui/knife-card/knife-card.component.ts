import { Component, Input } from '@angular/core';
import { KnifeBriefly } from '../../data/interfaces/knife-briefly.interface';
import { ImgUrlPipe } from '../../helpers/pipes/img-url.pipe';

@Component({
  selector: 'app-knife-card',
  imports: [ImgUrlPipe],
  templateUrl: './knife-card.component.html',
  styleUrl: './knife-card.component.scss'
})
export class KnifeCardComponent {
  @Input() knife!: KnifeBriefly;
}
