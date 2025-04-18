import { Component, inject, Input } from '@angular/core';
import { ImgUrlPipe } from '../../helpers/pipes/img-url.pipe';
import { KnifeBriefly } from '../../data/interfaces/knife.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-knife-card',
  imports: [ImgUrlPipe],
  templateUrl: './knife-card.component.html',
  styleUrl: './knife-card.component.scss'
})
export class KnifeCardComponent {
  @Input() knife!: KnifeBriefly;
  router = inject(Router)

  onClickCard() {
    this.router.navigate([`knife/${this.knife.id}`])
  }
}
