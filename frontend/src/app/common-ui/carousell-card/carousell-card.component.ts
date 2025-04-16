import { Component, Input } from '@angular/core';
import { ImgUrlPipe } from '../../helpers/pipes/img-url.pipe';
import { KnifeBriefly } from '../../data/interfaces/knife.interface';

@Component({
  selector: 'app-carousell-card',
  imports: [ImgUrlPipe],
  templateUrl: './carousell-card.component.html',
  styleUrl: './carousell-card.component.scss'
})
export class CarousellCardComponent {
  @Input() knife!: KnifeBriefly;
}
