import { Component, inject, OnInit } from '@angular/core';
import { SlickCarouselModule } from 'ngx-slick-carousel';
import { KnifeService } from '../../data/services/knife.service';
import { CarousellCardComponent } from "../carousell-card/carousell-card.component";
import { KnifeBriefly } from '../../data/interfaces/knife.interface';

@Component({
  selector: 'app-carousell',
  imports: [CarousellCardComponent, SlickCarouselModule, CarousellCardComponent],
  templateUrl: './carousell.component.html',
  styleUrl: './carousell.component.scss'
})
export class CarousellComponent implements OnInit {
  knifeService = inject(KnifeService)
  knifesBriefly: KnifeBriefly[] = []

  ngOnInit() {
    this.knifeService.getKnifesOnSale()
      .subscribe(knife => {
        this.knifesBriefly = knife
    })
  }

  slideConfig = {
    slidesToShow: 3,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 2000,
    infinite: true,
    arrows: true,
    responsive: [
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
    ]
  };
}
