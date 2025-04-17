import { Component, inject, Input } from '@angular/core';
import { ImgUrlPipe } from '../../helpers/pipes/img-url.pipe';
import { KnifeBriefly } from '../../data/interfaces/knife.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-carousell-card',
  imports: [ImgUrlPipe],
  templateUrl: './carousell-card.component.html',
  styleUrl: './carousell-card.component.scss'
})
export class CarousellCardComponent {
  @Input() knife!: KnifeBriefly;
  router = inject(Router)
  
  onClickCard() {
    this.router.navigate([`knife/${this.knife.id}`])
  }
}
