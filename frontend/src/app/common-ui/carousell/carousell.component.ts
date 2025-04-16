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
  ngOnInit() {
    // this.knifeService.getKnifesPaginated()
    //   .subscribe(val => {
    //     this.knifesBriefly = val
    // })
  }

  knifesBriefly: KnifeBriefly[] = [
    { image: '/assets/svg/logo-big.png', category: 'Category 1', id: 1, isOnSale: true, price: 400, title: 'Noj'},
    { image: '/assets/svg/logo-big.png', category: 'Category 1', id: 2, isOnSale: true, price: 400, title: 'Noj'},
    { image: '/assets/svg/logo-big.png', category: 'Category 1', id: 3, isOnSale: true, price: 400, title: 'Noj'},
    { image: '/assets/svg/logo-big.png', category: 'Category 1', id: 4, isOnSale: true, price: 400, title: 'Noj'},
  ];

  slideConfig = {
    slidesToShow: 3,
    slidesToScroll: 1,
    dots: true,
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
